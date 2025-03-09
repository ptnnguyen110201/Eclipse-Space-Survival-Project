using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class EnemyManager : FuncManager
{

    private static EnemyManager instance;
    public static EnemyManager Instance => Instance1;

    public static EnemyManager Instance1 { get => instance; set => instance = value; }

    [SerializeField] private Transform PlayerPos;
    private Quadtree<EnemyCtrl> enemyQuadtree;
    private Rect quadtreeBounds;

    [SerializeField] private Camera mainCamera;
    private Rect cameraViewBounds;


    private const int GROUP_SIZE = 50;
    protected override void Awake()
    {
        if (Instance1 != null)
        {
            Debug.LogError("Only one EnemyManager allowed to exist.");
            return;
        }

        Instance1 = this;
        // Initialize Quadtree với giới hạn bản đồ 1080x1920
        this.quadtreeBounds = new Rect(-540, -960, 1080, 1920);
        this.enemyQuadtree = new Quadtree<EnemyCtrl>(quadtreeBounds, 5);

        this.mainCamera = Camera.main;
    }

    protected override void Start()
    {
        base.Start();
        this.StartCoroutine(UpdateEnemyVisibility());
        this.StartCoroutine(MoveAllEnemiesCoroutine());

    }

    public void SetPlayerPos(Transform playerPos) => PlayerPos = playerPos;

    public void RegisterEnemy(EnemyCtrl enemy)
    {
        if (enemy == null) return;
        this.enemyQuadtree.Insert(enemy);
    }

    public void UnregisterEnemy(EnemyCtrl enemy)
    {
        if (enemy == null) return;
        this.enemyQuadtree.Remove(enemy);
    }

    private IEnumerator MoveAllEnemiesCoroutine()
    {
        while (true)
        {
    
            List<EnemyCtrl> allEnemies = enemyQuadtree.Query(quadtreeBounds);

    
            if (allEnemies.Count == 0)
            {
                yield return null;
            }
            for (int i = 0; i < allEnemies.Count; i += GROUP_SIZE)
            {
                int count = Mathf.Min(GROUP_SIZE, allEnemies.Count - i);
                this.ProcessMovementGroup(allEnemies.GetRange(i, count));
                yield return null;
            }
        }
    }

    private void ProcessMovementGroup(List<EnemyCtrl> group)
    {
        foreach (var enemy in group)
        {
            if (enemy == null || !enemy.gameObject.activeSelf) continue;
            enemy.GetEnemyMove().MoveTowardsPlayer(PlayerPos);
        }
    }

    private IEnumerator UpdateEnemyVisibility()
    {
     

        while (true)
        {
            UpdateCameraViewBounds();
            List<EnemyCtrl> visibleEnemies = enemyQuadtree.Query(cameraViewBounds);
            if (visibleEnemies.Count == 0)
            {
                yield return null;
            }
            for (int i = 0; i < visibleEnemies.Count; i += GROUP_SIZE)
            {
                int count = Mathf.Min(GROUP_SIZE, visibleEnemies.Count - i);
                this.ProcessVisibilityGroup(visibleEnemies.GetRange(i, count));
                yield return null;
            }
        }
    }
    private void ProcessVisibilityGroup(List<EnemyCtrl> group)
    {
        foreach (var enemy in group)
        {
            if (enemy == null) continue;

            Vector3 position = enemy.transform.position;
            bool isVisible = cameraViewBounds.Contains(new Vector2(position.x, position.y));

            if (enemy.GetObjectModel().GetSpriteRenderer() != isVisible)
            {
                enemy.GetObjectModel().SetVisibility(isVisible);
            }
        }
    }

    private void UpdateCameraViewBounds()
    {
        if (this.mainCamera == null) return;

        Vector3 cameraBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 cameraTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        cameraViewBounds = new Rect(cameraBottomLeft.x, cameraBottomLeft.y,
            cameraTopRight.x - cameraBottomLeft.x,
            cameraTopRight.y - cameraBottomLeft.y);
    }
}

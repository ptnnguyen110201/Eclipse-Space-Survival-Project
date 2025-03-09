using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance => instance;

    private Quadtree<ItemCtrl> itemQuadtree;
    private Rect quadtreeBounds;

    [SerializeField] private Camera mainCamera;
    private Rect cameraViewBounds;

    private const int GROUP_SIZE = 200;
    private const float VISIBILITY_UPDATE_INTERVAL = 0.5f;

    private NativeArray<Vector3> positions;
    private NativeArray<bool> results;

    private Vector3 lastCameraPosition;
    private List<ItemCtrl> allItems = new List<ItemCtrl>();

    protected void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only one ItemManager allowed to exist.");
            return;
        }

        instance = this;
        quadtreeBounds = new Rect(-540, -960, 1080, 1920);
        itemQuadtree = new Quadtree<ItemCtrl>(quadtreeBounds, 1);

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    protected void Start()
    {

        DOTween.SetTweensCapacity(10000, 200);
        InitializeNativeArrays(GROUP_SIZE);
        UpdateCameraViewBounds();
        StartCoroutine(UpdateVisibilityCoroutine());
    }

    private void InitializeNativeArrays(int maxSize)
    {
        positions = new NativeArray<Vector3>(maxSize, Allocator.Persistent);
        results = new NativeArray<bool>(maxSize, Allocator.Persistent);
    }

    private void DisposeNativeArrays()
    {
        if (positions.IsCreated) positions.Dispose();
        if (results.IsCreated) results.Dispose();
    }

    public void RegisterItem(ItemCtrl item)
    {
        if (item == null) return;
        allItems.Add(item);
        itemQuadtree.Insert(item);
    }

    public void UnregisterItem(ItemCtrl item)
    {
        if (item == null) return;
        allItems.Remove(item);
        itemQuadtree.Remove(item);
    }

  

    private IEnumerator UpdateVisibilityCoroutine()
    {
        while (true)
        {
            UpdateCameraViewBounds();

            List<ItemCtrl> visibleItems = itemQuadtree.Query(cameraViewBounds);

            if (visibleItems.Count > 0)
            {
                int totalItems = visibleItems.Count;
                int processed = 0;

                while (processed < totalItems)
                {
                    int batchSize = Mathf.Min(GROUP_SIZE, totalItems - processed);

                    for (int i = 0; i < batchSize; i++)
                    {
                        positions[i] = visibleItems[processed + i].transform.position;
                    }

                    VisibilityJob visibilityJob = new VisibilityJob
                    {
                        ItemPositions = positions,
                        ViewBounds = cameraViewBounds,
                        Results = results
                    };

                    JobHandle jobHandle = visibilityJob.Schedule(batchSize, 64);
                    jobHandle.Complete();

                    for (int i = 0; i < batchSize; i++)
                    {
                        if (visibleItems[processed + i].GetItemModel().GetSpriteRenderer().enabled == results[i]) continue;
                        visibleItems[processed + i].GetItemModel().SetVisibility(results[i]);
                    }

                    processed += batchSize;
                    yield return null;
                }
            }

            yield return new WaitForSeconds(VISIBILITY_UPDATE_INTERVAL);
        }
    }

    private void UpdateCameraViewBounds()
    {
        if (mainCamera == null) return;

        if (lastCameraPosition == mainCamera.transform.position) return;
        lastCameraPosition = mainCamera.transform.position;

        Vector3 cameraBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 cameraTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
        cameraViewBounds = new Rect(cameraBottomLeft.x, cameraBottomLeft.y,
            cameraTopRight.x - cameraBottomLeft.x,
            cameraTopRight.y - cameraBottomLeft.y);
    }

    protected void OnDestroy()
    {
        DisposeNativeArrays();
    }

}

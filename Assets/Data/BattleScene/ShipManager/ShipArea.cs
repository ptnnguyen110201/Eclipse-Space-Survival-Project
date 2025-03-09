using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipArea : ShipAbstract
{
    [SerializeField] protected List<Transform> enemiesInRange;
    [SerializeField] protected Vector2 checkSize;

    [SerializeField] private float detectionRadius = 0.25f; 
    [SerializeField] private LayerMask itemLayer; 

    protected override void OnEnable()
    {
        base.OnEnable();
        this.StartCoroutine(CheckEnemiesInRangeCoroutine());
        this.StartCoroutine(CheckItemsInRangeCoroutine()); 
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.StopCoroutine(CheckEnemiesInRangeCoroutine());
        this.StopCoroutine(CheckItemsInRangeCoroutine()); 
    }

    private IEnumerator CheckEnemiesInRangeCoroutine()
    {
        while (true)
        {
            this.UpdateBoxSize();
            this.UpdateEnemiesInRange();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator CheckItemsInRangeCoroutine()
    {
        while (true)
        {
            this.UpdateItemsInRange();
            yield return new WaitForSeconds(0.1f); 
        }
    }

    private void UpdateBoxSize()
    {
        float cameraHeight = ResolutionManager.Instance.MaxY;
        float cameraWidth = ResolutionManager.Instance.MaxX;
        this.checkSize = new Vector2(cameraWidth, cameraHeight);
    }

    private void UpdateEnemiesInRange()
    {
        this.enemiesInRange.Clear();
        Vector3 shipPos = this.shipCtrl.GetCircleCollider2D().transform.position;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(shipPos, this.checkSize, 0f, LayerMask.GetMask("Enemy"));

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.transform.parent.CompareTag("Enemy") || collider.transform.parent.CompareTag("Boss"))
                this.enemiesInRange.Add(collider.transform);
        }
    }

    private void UpdateItemsInRange()
    {
        Vector3 shipPos = this.shipCtrl.GetCircleCollider2D().transform.position;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(shipPos, this.detectionRadius, this.itemLayer);

        foreach (Collider2D collider in hitColliders)
        {
            ItemImpart item = collider.GetComponentInChildren<ItemImpart>();
            if (item != null) 
            {
                item.StartCoroutine(item.FlyOutAndReturn(this.shipCtrl.transform));
            }
        }
    }

    public Transform GetNearestEnemy()
    {
        Transform nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (Transform enemy in this.enemiesInRange)
        {
            if (enemy == null) continue;
            float distance = Vector2.Distance(transform.parent.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        return nearestEnemy;
    }

  
}

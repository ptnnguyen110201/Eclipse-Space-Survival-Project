using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupSpawner : Spawner
{
    private static ItemPickupSpawner instance;
    public static ItemPickupSpawner Instance => instance;

    [SerializeField] protected float gameDropRate = 1;

    protected override void Awake()
    {
        base.Awake();
        if (ItemPickupSpawner.instance != null) Debug.LogError("Only 1 ItemPickupSpawner allow to exist");
        ItemPickupSpawner.instance = this;
    }

    public virtual void ItemSpawn(ItemType itemType, Vector3 Pos, Quaternion Rot) 
    {
        Transform itemdrop = this.Spawn(itemType.ToString(), Pos, Rot);
        itemdrop.gameObject.SetActive(true);
    }

    public virtual List<ItemDropRate> Drop(List<ItemDropRate> dropList, Vector3 pos, Quaternion rot)
    {
        List<ItemDropRate> dropItems = new();

        if (dropList.Count < 1) return dropItems;

        dropItems = this.DropItems(dropList);

        foreach (ItemDropRate itemDropRate in dropItems)
        {
            Vector3 expSpawnPosition = GetRandomizedSpawnPosition(pos);

            Transform itemdrop = this.Spawn(itemDropRate.itemData.name, expSpawnPosition, rot);
            if (itemdrop == null) continue;
            ItemCtrl itemCtrl = itemdrop.GetComponent<ItemCtrl>();
            itemdrop.gameObject.SetActive(true);
        
        }

        return dropItems;
    }

    protected virtual List<ItemDropRate> DropItems(List<ItemDropRate> items)
    {
        List<ItemDropRate> droppedItems = new();

        foreach (ItemDropRate item in items)
        {
            float rate = Random.Range(0f, 1f);
            float itemRate = item.dropRate / 100f * GameDropRate();
            if (rate <= itemRate)
            {
                droppedItems.Add(item);
            }
        }

        return droppedItems;
    }

    protected virtual Vector3 GetRandomizedSpawnPosition(Vector3 originalPosition)
    {
        float offset = 0.5f;
        float offsetX = Random.Range(-offset, offset);
        float offsetY = Random.Range(-offset, offset);
        return new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, 0f);
    }
    protected virtual float GameDropRate()
    {
        return this.gameDropRate;
    }


}

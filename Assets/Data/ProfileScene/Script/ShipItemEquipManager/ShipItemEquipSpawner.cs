using System.Collections.Generic;
using UnityEngine;

public class ShipItemEquipSpawner : Spawner
{
    protected override void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Viewport/Content");
        Debug.Log(transform.name + " Load Holder ", gameObject);
    }

    public void SpawnItem(List<ShipItemEquipData> shipItemEquipDatas, SortType sortType)
    {

        List<ShipItemEquipData> sortedItems = SortManager.SortItems(shipItemEquipDatas, sortType);
        this.ClearItem();

        if (sortedItems.Count <= 0) return;


        for (int i = 0; i < sortedItems.Count; i++)
        {
            Transform itemTransform = this.Spawn("ItemEquipSlot", Vector3.zero, Quaternion.identity);
            itemTransform.gameObject.SetActive(true);
            itemTransform.localScale = Vector3.one;
            ShipItemEquipSlot shipItemEquipSlot = itemTransform.GetComponent<ShipItemEquipSlot>();
            shipItemEquipSlot.SetItemUI(sortedItems[i]);
            itemTransform.SetParent(this.holder);
            itemTransform.SetAsLastSibling(); 
        }
        this.SortItem(sortedItems);
    }

    private void SortItem(List<ShipItemEquipData> shipItemEquipDatas)
    {
       
        for (int i = 0; i < this.holder.childCount; i++)
        {
            Transform itemTransform = holder.GetChild(i);
            itemTransform.SetSiblingIndex(i); 
        }
    }

    public void ClearItem()
    {
        foreach (Transform obj in this.holder)
        {
            this.Despawn(obj);
        }
    }
}

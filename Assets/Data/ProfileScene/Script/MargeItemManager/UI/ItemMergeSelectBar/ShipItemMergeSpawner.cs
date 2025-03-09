using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipItemMergeSpawner : Spawner
{
    protected override void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Scroll View/Viewport/Content");
        Debug.Log(transform.name + " Load Holder ", gameObject);
    }

    public void SpawnItem(List<ShipItemEquipData> selectedItem, List<ShipItemEquipData> shipItemEquipDatas, SortType sortType)
    {
        List<ShipItemEquipData> sortedItems = SortManager.SortItems(shipItemEquipDatas, sortType);
        ClearItem();

        if (sortedItems.Count <= 0) return;

        ShipItemEquipData targetItem = selectedItem != null && selectedItem.Count > 0 ? selectedItem[0] : null;

        foreach (ShipItemEquipData sortedItem in sortedItems)
        {
            if (sortedItem.GetMaxTier()) continue;
            Transform itemTransform = Spawn("ItemMergeSlot", Vector3.zero, Quaternion.identity);
            itemTransform.gameObject.SetActive(true);
            itemTransform.localScale = Vector3.one;

            ShipItemMergeSlot shipItemMergeSlot = itemTransform.GetComponent<ShipItemMergeSlot>();
            shipItemMergeSlot.SetItemUI(sortedItem, selectedItem);

            if (targetItem != null)
            {
                bool isMatchingItem = IsMatchingItem(sortedItem, targetItem);
                shipItemMergeSlot.gameObject.SetActive(isMatchingItem);
            }

            itemTransform.SetParent(this.holder);
            itemTransform.SetAsLastSibling();
        }

        SortItem();
    }
    private bool IsMatchingItem(ShipItemEquipData sortedItem, ShipItemEquipData targetItem)
    {
        return sortedItem.shipItemData.shipItemType == targetItem.shipItemData.shipItemType &&
                sortedItem.shipItemData.ItemID == targetItem.shipItemData.ItemID &&
               sortedItem.currentTier == targetItem.currentTier;
    }



    private void SortItem()
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

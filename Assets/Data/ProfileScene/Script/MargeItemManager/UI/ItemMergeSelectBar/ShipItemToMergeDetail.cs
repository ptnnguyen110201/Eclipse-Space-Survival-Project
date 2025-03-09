using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipItemToMergeDetail : FuncManager
{
    [SerializeField] protected ShipItemMergeSlot itemMergeSlot;
    [SerializeField] protected List<ShipItemMergeSlot> requiredItemsSlots;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMergeItemSlot();
        this.LoadRequiredItemsSlots();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.ResetItemSlots();
    }
    public void ShowUI(List<ShipItemEquipData> requiredItems)
    {
        if (requiredItems == null || requiredItems.Count <= 0) return;
        this.ShowRequiredItems(requiredItems);
    }
    private void ShowRequiredItems(List<ShipItemEquipData> requiredItems)
    {
        ResetItemSlots();

        ShipItemEquipData shipItemEquipData = requiredItems[0];
        int ingredientsAmount = shipItemEquipData.GetShipItemTierData().ingredients.Amount;
        this.itemMergeSlot.SetItemUI(shipItemEquipData, requiredItems);
        this.itemMergeSlot.gameObject.SetActive(true);

        for (int i = 0; i < ingredientsAmount && i < this.requiredItemsSlots.Count; i++)
        {
            this.requiredItemsSlots[i].transform.parent.gameObject.SetActive(true);
        }

        for (int i = 1; i < requiredItems.Count && i - 1 < this.requiredItemsSlots.Count; i++)
        {
            this.requiredItemsSlots[i - 1].SetItemUI(requiredItems[i], requiredItems);
            this.requiredItemsSlots[i - 1].gameObject.SetActive(true);
        }
    }
    protected virtual void LoadRequiredItemsSlots()
    {
        if (requiredItemsSlots.Count > 0) return;

        foreach (Transform obj in transform.Find("Item_Required"))
        {
            ShipItemMergeSlot requiredSlot = obj.GetComponentInChildren<ShipItemMergeSlot>(true);
            if (requiredSlot != null)
            {
                this.requiredItemsSlots.Add(requiredSlot);
            }
        }
        Debug.Log(transform.name + " Loaded Required Items: " + requiredItemsSlots.Count, gameObject);
    }

    protected virtual void LoadMergeItemSlot()
    {
        if (itemMergeSlot != null) return;
        this.itemMergeSlot = transform.Find("Item_Merge").GetComponentInChildren<ShipItemMergeSlot>(true);
        Debug.Log(transform.name + " Loaded Merge Item Slot: " + gameObject);
    }
    public void ResetItemSlots()
    {
        this.itemMergeSlot.gameObject.SetActive(false);
        if (this.requiredItemsSlots.Count <= 0) return;
        foreach (ShipItemMergeSlot slot in this.requiredItemsSlots)
        {
            slot.transform.parent.gameObject.SetActive(false);
            slot.transform.gameObject.SetActive(false);
        }

    }
}

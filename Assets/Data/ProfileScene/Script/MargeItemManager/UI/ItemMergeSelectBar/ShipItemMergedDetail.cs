using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipItemToMergedDetail : FuncManager
{
    [SerializeField] protected ShipItemMergedSlot itemMergedSlot;
    [SerializeField] protected Transform Item_MergedDetail;
    [SerializeField] protected TextMeshProUGUI Item_Name;
    [SerializeField] protected TextMeshProUGUI Item_Level;
    [SerializeField] protected TextMeshProUGUI Item_nextLevel;
    [SerializeField] protected TextMeshProUGUI Item_BoosteAmount;
    [SerializeField] protected TextMeshProUGUI Item_nextBoosteAmount;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.ResetItemSlots();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem_MergedDetail();
        this.LoadShipItemMergedSlot();
        this.LoadItem_Name();
        this.LoadItem_Level();
        this.LoadItem_BoosteAmount();
    }

    public void ShowUI(List<ShipItemEquipData> requiredItems)
    {
        if (requiredItems == null || requiredItems.Count <= 0) return;
        this.ShowMergedItem(requiredItems);
    }

    private void ShowMergedItem(List<ShipItemEquipData> requiredItems)
    {
        this.ResetItemSlots();
        ShipItemEquipData currentItem = requiredItems[0];
        if (currentItem == null) return;
        ShipItemEquipData nextItem = new ()
        {
            shipItemData = currentItem.shipItemData,
            currentTier = currentItem.currentTier + 1,
        };
        this.Item_MergedDetail.gameObject.SetActive(true);
        this.itemMergedSlot.gameObject.SetActive(true);
        this.itemMergedSlot.SetItemUI(currentItem);
        if(nextItem == null) return;

        this.Item_Name.text = $"{currentItem.shipItemData.itemName} Tier {nextItem.currentTier}";

        this.Item_Level.text = $"Level Max {currentItem.GetMaxLevel()}";
        this.Item_nextLevel.text = $"{nextItem.GetMaxLevel()}";    

        this.Item_BoosteAmount.text = $"Boosted {currentItem.GetShipItemTierData().itemtierBoostAmount}%";
        this.Item_nextBoosteAmount.text = $"{nextItem.GetShipItemTierData().itemtierBoostAmount}%";

    }

    protected virtual void LoadItem_Name()
    {
        if (this.Item_Name != null) return;
        this.Item_Name = transform.Find("Item_MergedDetail/Item_Name").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load Item_Name", gameObject);
    }

    protected virtual void LoadItem_Level()
    {
        if (this.Item_Level != null && this.Item_nextLevel != null) return;
        this.Item_Level = transform.Find("Item_MergedDetail/Item_LvMax/item_currenValue").GetComponent<TextMeshProUGUI>();
        this.Item_nextLevel = transform.Find("Item_MergedDetail/Item_LvMax/item_nextValue").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load Item_Level", gameObject);
    }
   
    protected virtual void LoadItem_BoosteAmount()
    {
        if (this.Item_BoosteAmount != null) return;
        this.Item_BoosteAmount = transform.Find("Item_MergedDetail/Item_Boosted/item_currenValue").GetComponent<TextMeshProUGUI>();
        this.Item_nextBoosteAmount = transform.Find("Item_MergedDetail/Item_Boosted/item_nextValue").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load Item_BoosteAmount", gameObject);
    }

    protected virtual void LoadShipItemMergedSlot()
    {
        if (itemMergedSlot != null) return;
        this.itemMergedSlot = transform.Find("Item_Merged").GetComponentInChildren<ShipItemMergedSlot>(true);
        Debug.Log(transform.name + " Load ShipItemMergedSlot " + gameObject);
    }
    protected virtual void LoadItem_MergedDetail() 
    {
        if (this.Item_MergedDetail != null) return;
        this.Item_MergedDetail = transform.Find("Item_MergedDetail").GetComponent<Transform>();
        Debug.Log(transform.name + "Load Item_MergedDetail ", gameObject);
    }
    public void ResetItemSlots()
    {
        this.Item_MergedDetail.gameObject.SetActive(false); 
        this.itemMergedSlot.gameObject.SetActive(false);
        this.Item_Name.text = string.Empty;
        this.Item_Level.text = string.Empty;
        this.Item_nextLevel.text = string.Empty;
        this.Item_BoosteAmount.text = string.Empty;
        this.Item_nextBoosteAmount.text = string.Empty;
    }
}

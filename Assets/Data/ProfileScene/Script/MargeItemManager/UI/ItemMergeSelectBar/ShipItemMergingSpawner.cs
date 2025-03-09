using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipItemMergingSpawner : FuncManager
{

    [SerializeField] protected ShipItemToMergedDetail ShipItemToMergedDetail;
    [SerializeField] protected ShipItemToMergeDetail ShipItemToMergeDetail;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemToMergeDetail();
        this.LoadShipItemToMergedDetail();

    }
    public void ShowMergeUI(List<ShipItemEquipData> requiredItems)
    {
        this.ResetMergeUI();
        this.ShipItemToMergedDetail.ShowUI(requiredItems);
        this.ShipItemToMergeDetail.ShowUI(requiredItems);
    }

    public void ResetMergeUI() 
    {
        this.ShipItemToMergedDetail.ResetItemSlots();
        this.ShipItemToMergeDetail.ResetItemSlots();
    }
    protected virtual void LoadShipItemToMergedDetail()
    {
        if (this.ShipItemToMergedDetail != null) return;
        this.ShipItemToMergedDetail = transform.Find("ItemPanel").GetComponentInChildren<ShipItemToMergedDetail>(true);
        Debug.Log(transform.name + "Load ShipItemToMergedDetail " + gameObject);
    }
    protected virtual void LoadItemToMergeDetail()
    {
        if (this.ShipItemToMergeDetail != null) return;
        this.ShipItemToMergeDetail = transform.Find("ItemPanel").GetComponentInChildren<ShipItemToMergeDetail>(true);
        Debug.Log(transform.name + "Load ItemToMergeDetail " + gameObject);
    }


}

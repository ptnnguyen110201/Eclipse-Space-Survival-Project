using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEquipingUI : FuncManager
{
    [SerializeField] protected ShipItemEquipSlot main_Item;
    [SerializeField] protected List<ShipItemEquipSlot> support_Item;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMain_ItemSlot();
        this.LoadSupport_ItemSlot();


    }

    protected virtual void LoadSupport_ItemSlot()
    {
        if (this.support_Item.Count > 0) return;

        foreach (Transform obj in this.transform.Find("ItemSlot/Support_Item"))
        {
            ShipItemEquipSlot equipingSlot = obj.GetComponentInChildren<ShipItemEquipSlot>(true);
            this.support_Item.Add(equipingSlot);
        }
        Debug.Log(transform.name + " loaded ShipItemEquipSlots: " + support_Item.Count, gameObject);      
        this.ResetItemSlot();
    }

    protected virtual void LoadMain_ItemSlot()
    {
        if (this.main_Item != null) return;
        this.main_Item = this.transform.Find("ItemSlot/Main_Item").GetComponentInChildren<ShipItemEquipSlot>(true);
        Debug.Log(transform.name + " loaded Main Item Slot: " + gameObject);
    }
    public virtual void SpawnItemEquiping(List<ShipItemEquipData> shipItemEquipDatas)
    {
        this.ResetItemSlot();
        if (shipItemEquipDatas.Count <= 0) return;

        foreach (ShipItemEquipData shipItemEquip in shipItemEquipDatas)
        {
            switch (shipItemEquip.shipItemData.shipItemType)
            {
                case ShipItemType.ShipHull:
                    this.main_Item.gameObject.SetActive(true);
                    this.main_Item.SetItemUI(shipItemEquip);
                    break;
                case ShipItemType.ShipGun:
                    this.SetUISupportItem(shipItemEquip);
                    break;
                case ShipItemType.ShipMissile:
                    this.SetUISupportItem(shipItemEquip);
                    break;
            }
        }
    }
    private void SetUISupportItem(ShipItemEquipData shipItemEquip) 
    {
        if (shipItemEquip == null) return;
        foreach(ShipItemEquipSlot slot in this.support_Item) 
        {
            if(slot.transform.parent.name != shipItemEquip.shipItemData.shipItemType.ToString()) continue;
            slot.gameObject.SetActive(true);
            slot.SetItemUI(shipItemEquip);
        }
    }
    private void ResetItemSlot() 
    {
        this.main_Item.gameObject.SetActive(false);
        if (this.support_Item.Count < 0) return;
        foreach(ShipItemEquipSlot shipItemEquipSlot in this.support_Item) 
        {
            shipItemEquipSlot.gameObject.SetActive(false);
        }
    }
}

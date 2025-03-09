using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipItemEquipSlot : ButtonBase
{
    Color32[] colors = new Color32[]
{
    new Color(0, 1, 0),
    new Color(0, 1, 1),
    new Color(0, 0, 1),
    new Color(1, 0, 0),
    new Color(1, 0, 1),
};
    [SerializeField] protected Image Item_Slot;
    [SerializeField] protected Image Item_Image;
    [SerializeField] protected TextMeshProUGUI Item_Level;
    [SerializeField] protected ShipItemEquipData shipItemEquipData;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem_Slot();
        this.LoadItem_Image();
        this.LoadItem_Level();
    }


    public virtual void SetItemUI(ShipItemEquipData shipItemEquipData) 
    {
        if (shipItemEquipData == null) return;
        this.shipItemEquipData = shipItemEquipData;
        this.Item_Image.sprite = shipItemEquipData.GetShipItemTierData().itemTierSprite;
        this.Item_Level.text = $"Lv.{shipItemEquipData.GetShipItemLevelData().itemLevel}";
        this.Item_Slot.color = colors[shipItemEquipData.currentTier - 1];
    }
    protected virtual void LoadItem_Slot()
    {
        if (this.Item_Slot != null) return;
        this.Item_Slot = transform.GetComponent<Image>();
        Debug.Log(transform.name + "LoadItem_Slot ", gameObject);
    }
    protected virtual void LoadItem_Image()
    {
        if (this.Item_Image != null) return;
        this.Item_Image = transform.Find("Item_Image").GetComponent<Image>();
        Debug.Log(transform.name + "Load Item_Image ", gameObject);
    }
    protected virtual void LoadItem_Level()
    {
        if (this.Item_Level != null) return;
        this.Item_Level = transform.Find("Item_Level").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + "Load Item_Level ", gameObject);
    }
   
    protected override void OnClick()
    {
        EquipmentBarManager.Instance.OpenStat(this.shipItemEquipData);
    }

}

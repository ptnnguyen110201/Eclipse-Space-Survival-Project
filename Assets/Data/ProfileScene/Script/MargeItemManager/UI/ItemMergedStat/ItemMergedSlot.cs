using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMergedSlot : FuncManager
{
    Color32[] colors = new Color32[]
{
    new Color(0, 1, 0, 1f),
    new Color(0, 1, 1, 1f),
    new Color(0, 0, 1 ,1f),
    new Color(1, 0, 0 ,1f),
    new Color(1, 0, 1, 1f),
}; 
    [SerializeField] protected Image Item_Slot;
    [SerializeField] protected Image Item_Image;
    [SerializeField] protected TextMeshProUGUI Item_Level;
    [SerializeField] protected ShipItemEquipData shipItemEquipData;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem_Image();
        this.LoadItem_Level();
        this.LoadItem_Slot();
    }


    public virtual void SetItemMergeUI(ShipItemEquipData shipItemEquipData)
    {
        this.ResetItemSlots();
        if (shipItemEquipData == null) return;
        this.shipItemEquipData = shipItemEquipData;
        this.Item_Level.text = $"Lv.{shipItemEquipData.GetShipItemLevelData().itemLevel}";
        this.Item_Image.sprite =  shipItemEquipData.GetShipItemTierData().itemTierSprite;
        this.Item_Slot.color = this.colors[shipItemEquipData.currentTier - 1];
    }
    public virtual void SetItemMergedUI(ShipItemEquipData shipItemEquipData)
    {
        this.ResetItemSlots();
        if (shipItemEquipData == null) return;
        ShipItemEquipData itemEquipData = new ShipItemEquipData
        {
            shipItemData = shipItemEquipData.shipItemData,
            currentLevel = shipItemEquipData.currentLevel,
            currentTier = shipItemEquipData.currentTier + 1,
        };
        this.shipItemEquipData = itemEquipData;
        this.Item_Level.text = $"Lv.{itemEquipData.GetShipItemLevelData().itemLevel}";
        this.Item_Image.sprite = itemEquipData.GetShipItemTierData().itemTierSprite;
        this.Item_Slot.color = this.colors[itemEquipData.currentTier - 1];
    }
    protected virtual void LoadItem_Slot()
    {
        if (this.Item_Slot != null) return;
        this.Item_Slot = transform.GetComponent<Image>();
        Debug.Log(transform.name + " Load Item_Slot ", gameObject);
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
    public void ResetItemSlots()
    {
        this.Item_Image.sprite = null;
        this.Item_Level.text = string.Empty;

    }
}

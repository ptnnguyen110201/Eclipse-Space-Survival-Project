using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipItemMergeSlot : ButtonBase
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
    [SerializeField] protected Image Item_StickImage;
    [SerializeField] protected TextMeshProUGUI Item_Level;
    [SerializeField] protected ShipItemEquipData shipItemEquipData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadItem_Slot();
        LoadItem_Image();
        LoadItem_Level();
        LoadItem_TickImage();
    }

    public virtual void SetItemUI(ShipItemEquipData shipItemEquipData, List<ShipItemEquipData> selectedItems)
    {
        if (shipItemEquipData == null) return;
        this.shipItemEquipData = shipItemEquipData;

        this.Item_Image.sprite = shipItemEquipData.GetShipItemTierData().itemTierSprite;
        this.Item_Level.text = $"Lv.{shipItemEquipData.GetShipItemLevelData().itemLevel}";
        this.Item_Slot.color = this.colors[shipItemEquipData.currentTier - 1];

        bool isSelected = selectedItems.Contains(shipItemEquipData);
        this.Item_StickImage.gameObject.SetActive(isSelected);
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
        Debug.Log(transform.name + " Load Item_Image ", gameObject);
    }

    protected virtual void LoadItem_Level()
    {
        if (this.Item_Level != null) return;
        this.Item_Level = transform.Find("Item_Level").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load Item_Level ", gameObject);
    }

    protected virtual void LoadItem_TickImage()
    {
        if (this.Item_StickImage != null) return;
        this.Item_StickImage = transform.Find("Item_TickImage").GetComponent<Image>();
        Debug.Log(transform.name + " Load Item_TickImage ", gameObject);
    }

   
    protected override void OnClick()
    {
        ShipItemMergeManager.Instance.SelectItemEquipData(this.shipItemEquipData);
    }
}

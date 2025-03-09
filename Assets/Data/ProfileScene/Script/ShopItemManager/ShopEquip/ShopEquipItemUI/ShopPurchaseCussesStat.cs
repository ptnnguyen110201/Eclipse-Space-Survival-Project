using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPurchaseCussesStat : ButtonBase
{
    Color[] colors = new Color[]
   {
    new Color(0.2f, 0.6f, 0.2f), 
    new Color(0.2f, 0.5f, 0.5f), 
    new Color(0.3f, 0.3f, 0.8f), 
    new Color(0.7f, 0.2f, 0.2f), 
    new Color(0.6f, 0.1f, 0.6f), 
   };
    [SerializeField] protected Image item_Slot;
    [SerializeField] protected Image item_Image;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem_Image();
    }
    public void ShowUI(ShopEquipItemData shopEquiItemData) 
    {
        if (shopEquiItemData == null) return;
        this.item_Image.sprite = shopEquiItemData.GetShipItemTierData().itemTierSprite;
        this.item_Slot.color = this.colors[shopEquiItemData.currentTier - 1];

    }
    protected virtual void LoadItem_Image() 
    {
        if (this.item_Image != null) return;
        this.item_Image = transform.Find("Item_Slot/Item_Image").GetComponent<Image>();
        this.item_Slot = transform.Find("Item_Slot").GetComponent<Image>();
        Debug.Log(transform.name + "Load Item_Image", gameObject);
    }
    protected override void OnClick()
    {
        ShopBarManager.Instance.CloseStat();
        ShopEquipItemManager.Instance.SetShopUI();
    }
}

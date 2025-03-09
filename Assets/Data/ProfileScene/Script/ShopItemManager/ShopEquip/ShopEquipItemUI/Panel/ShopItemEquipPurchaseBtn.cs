using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemEquipPurchaseBtn : ButtonBase
{
    [SerializeField] protected ShopEquipItemData ShopEquipItemData;
    [SerializeField] protected TextMeshProUGUI ItemPriceText;
    [SerializeField] protected Image ItemPriceImage;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemPriceTextAndImage();
    }

    protected virtual void LoadItemPriceTextAndImage()
    {
        if (this.ItemPriceText != null) return;
        this.ItemPriceText = transform.GetComponentInChildren<TextMeshProUGUI>();
        this.ItemPriceImage = transform.Find("ItemPrice_Image").GetComponent<Image>();
        Debug.Log(transform.transform + " Load ItemPriceTextAndImage", gameObject);
    }
    public virtual void ShowUI(ShopEquipItemData shopEquiItemData, bool EnoughCurrentcy)
    {
        if (shopEquiItemData == null) return;
        this.ShopEquipItemData = shopEquiItemData;
        this.ItemPriceImage.sprite = shopEquiItemData.shipItemData.playerGolds[0].goldsData.GoldsSprite;
        if(!EnoughCurrentcy)
        {
            this.button.interactable = false;
            this.ItemPriceText.text = this.FormatNumber(shopEquiItemData.GetGoldsData().Amount);
            return; 
        }
            this.button.interactable = true;
            this.ItemPriceText.text = this.FormatNumber(shopEquiItemData.GetGoldsData().Amount);
        
    }
    private string FormatNumber(int value) 
    {
        return NumberFormatter.FormatNumber(value);
    }
    protected override void OnClick()
    {
        ShopEquipItemManager.Instance.PurchaseItem(this.ShopEquipItemData);
        ShopBarManager.Instance.OpenSuccesStat(this.ShopEquipItemData);
    }
}

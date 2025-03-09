using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopEquipItemSlot : ButtonBase
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
    [SerializeField] protected TextMeshProUGUI item_Name;
    [SerializeField] protected Image item_Image;
    [SerializeField] protected Image item_PriceImage;
    [SerializeField] protected TextMeshProUGUI item_Price;
    [SerializeField] protected ShopEquipItemData ShopEquipItemData;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem_Slot();
        this.LoadItem_Name();
        this.Loaditem_Image();
        this.Loaditem_Price();
    }

    public void SetItem(ShopEquipItemData ShopEquipItemData, bool isPurchased)
    {
        if (ShopEquipItemData == null) return;

        this.ShopEquipItemData = ShopEquipItemData;
        this.item_Name.text = $"{ShopEquipItemData.shipItemData.itemName} {ShopEquipItemData.currentTier}";
        this.item_Image.sprite = ShopEquipItemData.GetShipItemTierData().itemTierSprite;
        this.item_PriceImage.sprite = ShopEquipItemData.shipItemData.playerGolds[0].goldsData.GoldsSprite;
        this.Item_Slot.color = colors[ShopEquipItemData.currentTier - 1];
        if (isPurchased)
        {
            this.item_PriceImage.gameObject.SetActive(false);
            this.button.interactable = false;
            this.item_Price.text = "Purchased";
            return;
        }
        this.item_PriceImage.gameObject.SetActive(true);
        this.button.interactable = true;
        this.item_Price.text = NumberFormatter.FormatNumber(ShopEquipItemData.GetGoldsData().Amount);

    }
    protected virtual void LoadItem_Slot()
    {
        if (this.Item_Slot != null) return;
        this.Item_Slot = transform.GetComponent<Image>();
        Debug.Log(transform.name + "LoadItem_Slot ", gameObject);
    }
    protected virtual void LoadItem_Name()
    {
        if (this.item_Name != null) return;
        this.item_Name = transform.Find("item_Name").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load Item_Name", gameObject);
    }

    protected virtual void Loaditem_Image()
    {
        if (this.item_Image != null) return;
        this.item_Image = transform.Find("item_Image").GetComponentInChildren<Image>();
        Debug.Log(transform.name + " Load item_Image", gameObject);

    }
    protected virtual void Loaditem_Price()
    {
        if (this.item_Price != null) return;
        this.item_PriceImage = transform.Find("item_Price/Image").GetComponent<Image>();
        this.item_Price = transform.Find("item_Price/Value").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load item_Price", gameObject);
    }


    protected override void OnClick()
    {
       this.SetItem(this.ShopEquipItemData, this.ShopEquipItemData.IsPurchased);
        ShopBarManager.Instance.OpenStat(this.ShopEquipItemData); 
       
    }
}

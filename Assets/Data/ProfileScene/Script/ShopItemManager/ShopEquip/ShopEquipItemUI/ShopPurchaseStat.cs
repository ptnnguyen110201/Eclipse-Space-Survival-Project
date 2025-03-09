using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPurchaseStat : FuncManager
{
    Color[] colors = new Color[]
   {
    new Color(0.2f, 0.6f, 0.2f), // Xanh lá cây
    new Color(0.2f, 0.5f, 0.5f), // Xanh lam
    new Color(0.3f, 0.3f, 0.8f), // Xanh dương
    new Color(0.7f, 0.2f, 0.2f), // Đỏ
    new Color(0.6f, 0.1f, 0.6f), // Tím
   };


    [SerializeField] protected TextMeshProUGUI item_Name;
    [SerializeField] protected ShopItemEquipContainer ShopItemEquipContainer;
    [SerializeField] protected ShopItemEquipDescription shopItemEquipDescription;
    [SerializeField] protected ShopItemEquipPurchaseBtn shopItemEquipPurchase;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem_Name();
        this.LoadShopItemEquipContainer();

        this.LoadShopItemEquipDescription();
        this.LoadShopItemEquipPurchaseBtn();
    }

    public void SetItem(ShopEquipItemData ShopEquipItemData, PlayerData playerData)
    {
        if (ShopEquipItemData == null) return;

        bool EnoughtCurrentcy = playerData.currencyData.HasEnoughCurrency(ShopEquipItemData.GetGoldsData().Amount, CurrencyType.Golds);
        this.SetUIItemName(ShopEquipItemData);

        this.ShopItemEquipContainer.ShowUI(ShopEquipItemData);
        this.shopItemEquipDescription.ShowUI(ShopEquipItemData, this.colors);
        this.shopItemEquipPurchase.ShowUI(ShopEquipItemData, EnoughtCurrentcy);



    }
    private void SetUIItemName(ShopEquipItemData ShopEquipItemData)
    {
        string itemName = ShopEquipItemData.shipItemData.itemName;
        string ItemTier = ShopEquipItemData.currentTier.ToString();

        this.item_Name.text = $"{itemName} Tier {ItemTier}";
    }


    protected virtual void LoadItem_Name()
    {
        if (this.item_Name != null) return;
        this.item_Name = transform.Find("item_Name").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load Item_Name", gameObject);
    }
  
    protected virtual void LoadShopItemEquipContainer()
    {
        if (this.ShopItemEquipContainer != null) return;
        this.ShopItemEquipContainer = transform.GetComponentInChildren<ShopItemEquipContainer>();
        Debug.Log(transform.name + " Load ShopItemEquipContainer", gameObject);
    }
    protected virtual void LoadShopItemEquipDescription()
    {
        if (this.shopItemEquipDescription != null) return;
        this.shopItemEquipDescription = transform.GetComponentInChildren<ShopItemEquipDescription>();
        Debug.Log(transform.name + "Load ShopItemEquipDescription", gameObject);
    }
    protected virtual void LoadShopItemEquipPurchaseBtn()
    {
        if (this.shopItemEquipPurchase != null) return;
        this.shopItemEquipPurchase = transform.Find("Item_PurchaseBtn").GetComponentInChildren<ShopItemEquipPurchaseBtn>();
        Debug.Log(transform.name + "Load ShopItemEquipPurchaseBtn", gameObject);
    }
   
    private string FormatNumber(int value)
    {
        return NumberFormatter.FormatNumber(value);
    }
}

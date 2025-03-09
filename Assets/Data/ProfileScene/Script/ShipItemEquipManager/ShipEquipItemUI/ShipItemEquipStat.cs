using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipItemEquipStat : FuncManager
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
    [SerializeField] protected TextMeshProUGUI Item_LevelUpPrice;

    [SerializeField] protected ShipItemEquipContainer shipItemEquipContainer;
    [SerializeField] protected ShipItemEquipDescription ShipItemEquipDescription;

    [SerializeField] protected ShipItemEquipBtn ShipItemEquipBtn;
    [SerializeField] protected ShipItemEquipLevelUpBtn ShipItemEquipLevelUpBtn;
    [SerializeField] protected ShipItemEquipLevelUpMax ShipItemEquipLevelUpMax;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem_Name();
        this.LoadItem_LevelUpPrice();

        this.LoadShipItemEquipContainer();
        this.LoadShipItemEquipDescription();

        this.LoadShipItemEquipBtn();
        this.LoadShipItemEquipLevelUpBtn();
        this.LoadShipItemEquipLevelUpMax();
    }

    public void SetItem(ShipItemEquipData shipItemEquipData, PlayerData playerData)
    {
        if (shipItemEquipData == null) return;

        this.SetUIItemName(shipItemEquipData);
        this.SetUIUpgradePrice(shipItemEquipData, playerData);
        this.shipItemEquipContainer.ShowUI(shipItemEquipData);
        this.ShipItemEquipDescription.ShowUI(shipItemEquipData, this.colors);
        this.ShipItemEquipBtn.ShowUI(shipItemEquipData);
        this.ShipItemEquipLevelUpBtn.ShowUI(shipItemEquipData);
        this.ShipItemEquipLevelUpMax.ShowUI(shipItemEquipData);
    }
    public void ShowUgpradeEffect(ShipItemEquipData shipItemEquipData) => this.shipItemEquipContainer.ShowUIUpgrade(shipItemEquipData);
    
    private void SetUIItemName(ShipItemEquipData shipItemEquipData)
    {
        string itemName = shipItemEquipData.shipItemData.itemName;
        string ItemTier = shipItemEquipData.currentTier.ToString();

        this.item_Name.text = $"{itemName} Tier {ItemTier}";
    }

    private void SetUIUpgradePrice(ShipItemEquipData shipItemEquipData, PlayerData playerData)
    {
        string currentPrice = this.FormatNumber(playerData.currencyData.playerGolds.Amount);
        string upgradePrice = this.FormatNumber(shipItemEquipData.GetShipItemLevelData().Ingredients.Amount);
        this.Item_LevelUpPrice.text = $"{currentPrice}/{upgradePrice}";
    }
    protected virtual void LoadItem_Name()
    {
        if (this.item_Name != null) return;
        this.item_Name = transform.Find("item_Name").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load Item_Name", gameObject);
    }
    protected virtual void LoadItem_LevelUpPrice()
    {
        if (this.Item_LevelUpPrice != null) return;
        this.Item_LevelUpPrice = transform.Find("Item_LevelUpPrice").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " Item_LevelUpPrice", gameObject);
    }
    protected virtual void LoadShipItemEquipContainer()
    {
        if (this.shipItemEquipContainer != null) return;
        this.shipItemEquipContainer = transform.GetComponentInChildren<ShipItemEquipContainer>();
        Debug.Log(transform.name + " Load ShipItemEquipContainer", gameObject);
    }
    protected virtual void LoadShipItemEquipDescription()
    {
        if (this.ShipItemEquipDescription != null) return;
        this.ShipItemEquipDescription = transform.GetComponentInChildren<ShipItemEquipDescription>();
        Debug.Log(transform.name + "Load ShipItemEquipDescription", gameObject);
    }
    protected virtual void LoadShipItemEquipBtn()
    {
        if (this.ShipItemEquipBtn != null) return;
        this.ShipItemEquipBtn = transform.Find("Item_EquipBtn").GetComponentInChildren<ShipItemEquipBtn>();
        Debug.Log(transform.name + " Load ShipItemEquipBtn", gameObject);
    }
    protected virtual void LoadShipItemEquipLevelUpBtn()
    {
        if (this.ShipItemEquipLevelUpBtn != null) return;
        this.ShipItemEquipLevelUpBtn = transform.Find("Item_EquipBtn").GetComponentInChildren<ShipItemEquipLevelUpBtn>();
        Debug.Log(transform.name + "Load ShipItemEquipLevelUpBtn", gameObject);
    }
    protected virtual void LoadShipItemEquipLevelUpMax()
    {
        if (this.ShipItemEquipLevelUpMax != null) return;
        this.ShipItemEquipLevelUpMax = transform.Find("Item_EquipBtn").GetComponentInChildren<ShipItemEquipLevelUpMax>();
        Debug.Log(transform.name + "Load ShipItemEquipLevelUpMax", gameObject);
    }
    private string FormatNumber(int value)
    {
        return NumberFormatter.FormatNumber(value);
    }
}

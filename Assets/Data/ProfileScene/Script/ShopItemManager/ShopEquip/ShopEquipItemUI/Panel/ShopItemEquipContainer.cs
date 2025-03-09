using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemEquipContainer : FuncManager
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
    [SerializeField] protected Image item_Image;
    [SerializeField] protected TextMeshProUGUI item_Level;
    [SerializeField] protected TextMeshProUGUI item_ATK;
    [SerializeField] protected TextMeshProUGUI item_HP;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemContainer();
    }
    protected virtual void LoadItemContainer()
    {
        if (this.item_ATK != null && item_HP != null && this.item_Image != null) return;
        this.Item_Slot = transform.Find("Item_Image").GetComponent<Image>();
        this.item_Image = transform.Find("Item_Image/Item_Image").GetComponent<Image>();
        this.item_Level = transform.Find("Item_Image/Item_Level").GetComponent<TextMeshProUGUI>();
        this.item_ATK = transform.Find("Item_Detail/Item_Attributes/Item_ATK").GetComponentInChildren<TextMeshProUGUI>();
        this.item_HP = transform.Find("Item_Detail/Item_Attributes/Item_HP").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load item_Image", gameObject);
    }

    public void ShowUI(ShopEquipItemData ShopEquipItemData)
    {

        this.item_Image.sprite = ShopEquipItemData.GetShipItemTierData().itemTierSprite;
        this.item_Level.text = $"Lv.{ShopEquipItemData.currentLevel}/{ShopEquipItemData.GetMaxLevel()}";
        this.Item_Slot.color = this.colors[ShopEquipItemData.currentTier - 1];
        foreach (var attribute in ShopEquipItemData.GetShipItemLevelData().ShipItemAttributes)
        {
            bool isActive = attribute.Amount > 0;

            switch (attribute.Attribute)
            {
                case ShipItemAttributesCode.ATK:
                    this.item_ATK.text = attribute.Amount.ToString();
                    this.item_ATK.gameObject.SetActive(isActive);
                    break;

                case ShipItemAttributesCode.HP:
                    this.item_HP.text = attribute.Amount.ToString();
                    this.item_HP.gameObject.SetActive(isActive);
                    break;
            }
        }
    }
}

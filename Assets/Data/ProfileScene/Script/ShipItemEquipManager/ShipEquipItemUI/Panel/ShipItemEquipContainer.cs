using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipItemEquipContainer : FuncManager
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

    public void ShowUI(ShipItemEquipData shipItemEquipData)
    {
        this.item_Image.sprite = shipItemEquipData.GetShipItemTierData().itemTierSprite;
        this.item_Level.text = $"Lv.{shipItemEquipData.currentLevel}/{shipItemEquipData.GetMaxLevel()}";
        this.Item_Slot.color = this.colors[shipItemEquipData.currentTier - 1];
        foreach (var attribute in shipItemEquipData.GetShipItemLevelData().ShipItemAttributes)
        {
            switch (attribute.Attribute)
            {       
                case ShipItemAttributesCode.ATK:  
                 
                    this.item_ATK.text = $"{shipItemEquipData.currentATK}";
                    break;

                case ShipItemAttributesCode.HP:
                
                    this.item_HP.text = $"{shipItemEquipData.currentHP}";
                    break;
            }
        }
          
    }
    public void ShowUIUpgrade(ShipItemEquipData currentData)
    {
        UIManager.Instance.ScaleAnimation(item_Image.transform.parent.gameObject, 1.1f, 0.5f);    
        UIManager.Instance.ScaleAnimation(item_ATK.transform.parent.gameObject, 1.1f, 0.5f);      
       UIManager.Instance.ScaleAnimation(item_HP.transform.parent.gameObject, 1.1f, 0.5f);

 
        ShipItemEquipData nextData = new ShipItemEquipData
        {
            shipItemData = currentData.shipItemData,
            currentTier = currentData.currentTier,
            currentLevel = currentData.currentLevel + 1
        };

        ShipItemLevelData nextLevelData = nextData.GetShipItemLevelData();

        if (nextLevelData == null)
        {
            Debug.LogWarning("Next level data is not available.");
            return;
        }

        for (int i = 0; i < currentData.GetShipItemLevelData().ShipItemAttributes.Count; i++)
        {
            ShipItemAttributes currentAttribute = currentData.GetShipItemLevelData().ShipItemAttributes[i];
            ShipItemAttributes nextAttribute = nextLevelData.ShipItemAttributes[i];

            switch (currentAttribute.Attribute)
            {
                case ShipItemAttributesCode.HP:
                   UIManager.Instance.IncreaseValue(currentAttribute.Amount, nextAttribute.Amount, 0.5f, item_HP);
                    break;
                case ShipItemAttributesCode.ATK:
                   UIManager.Instance.IncreaseValue(currentAttribute.Amount, nextAttribute.Amount, 0.5f, item_ATK);
                    break;
            }
        }
    }

}

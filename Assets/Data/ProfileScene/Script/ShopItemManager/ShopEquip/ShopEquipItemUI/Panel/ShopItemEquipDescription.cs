using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemEquipDescription : FuncManager
{
    [SerializeField] protected List<TextMeshProUGUI> Item_Description;
    [SerializeField] protected List<Image> Item_DescriptionImage;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem_Description();

    }
    protected virtual void LoadItem_Description()
    {
        if (this.Item_Description.Count > 0 && this.Item_DescriptionImage.Count > 0) return;
        foreach (Transform item in transform)
        {
            TextMeshProUGUI text = item.GetComponentInChildren<TextMeshProUGUI>();
            Image image = item.GetComponentInChildren<Image>();

            if (text != null && image != null)
            {
                this.Item_Description.Add(text);
                this.Item_DescriptionImage.Add(image);
            }
        }
        Debug.Log(transform.name + " Load Item_Description", gameObject);
    }
    public void ShowUI(ShopEquipItemData ShopEquipItemData, Color[] colors)
    {
        List<ShipItemTierData> shipItemTierDatas = ShopEquipItemData.shipItemData.shipItemTierDatas;


        if (ShopEquipItemData.shipItemData.shipItemType == ShipItemType.ShipHull)
        {

            for (int i = 0; i < shipItemTierDatas.Count; i++)
            {
                Color selectedColor = colors[i % colors.Length];
                this.Item_DescriptionImage[i].sprite = shipItemTierDatas[i].itemTierSprite;


                if (i < ShopEquipItemData.currentTier)
                {
                    this.Item_Description[i].text = $"<color=#{ColorUtility.ToHtmlStringRGBA(selectedColor)}> Tier {shipItemTierDatas[i].itemTier}: Increase {shipItemTierDatas[i].itemtierBoostAmount}% for overall damage</color>\n";
                    continue;
                }

                this.Item_Description[i].text = string.Empty;

            }
        }
        else
        {
            for (int i = 0; i < shipItemTierDatas.Count; i++)
            {
                Color selectedColor = colors[i % colors.Length];
                this.Item_DescriptionImage[i].sprite = shipItemTierDatas[i].itemTierSprite;

                if (i < ShopEquipItemData.currentTier)
                {
                    this.Item_Description[i].text = $"<color=#{ColorUtility.ToHtmlStringRGBA(selectedColor)}> Tier {shipItemTierDatas[i].itemTier}: Increase {shipItemTierDatas[i].itemtierBoostAmount}% for the equipment</color>\n";
                    continue;
                }
                this.Item_Description[i].text = string.Empty;
            }
        }
    }
}

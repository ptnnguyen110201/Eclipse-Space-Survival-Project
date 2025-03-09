using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipItemEquipDescription : FuncManager
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
    public void ShowUI(ShipItemEquipData shipItemEquipData, Color[] colors)
    {
        List<ShipItemTierData> shipItemTierDatas = shipItemEquipData.shipItemData.shipItemTierDatas;

        for (int i = 0; i < Item_DescriptionImage.Count; i++)
        {
            if (i >= shipItemTierDatas.Count)
            {
                Item_DescriptionImage[i].gameObject.SetActive(false);
                Item_Description[i].gameObject.SetActive(false);
                continue;
            }

            Item_DescriptionImage[i].gameObject.SetActive(true);
            Item_Description[i].gameObject.SetActive(true);

            Item_DescriptionImage[i].sprite = shipItemTierDatas[i].itemTierSprite;

            Color selectedColor = colors[i % colors.Length];

            if (shipItemEquipData.shipItemData.shipItemType == ShipItemType.ShipHull)
            {
                if (i < shipItemEquipData.currentTier)
                {
                    Item_Description[i].text = $"<color=#{ColorUtility.ToHtmlStringRGBA(selectedColor)}> Tier {shipItemTierDatas[i].itemTier}: Increase {shipItemTierDatas[i].itemtierBoostAmount}% for all attributes</color>";
                    continue;
                }
                Item_Description[i].text = string.Empty;

                continue;
            }


            if (i < shipItemEquipData.currentTier)
            {
                Item_Description[i].text = $"<color=#{ColorUtility.ToHtmlStringRGBA(selectedColor)}> Tier {shipItemTierDatas[i].itemTier}: Increase {shipItemTierDatas[i].itemtierBoostAmount}% for the equipment</color>";
                continue;
            }
            Item_Description[i].text = string.Empty;
            continue;

        }

    }
}
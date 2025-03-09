using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipItemData", menuName = "Ship/ShipItemData")]
public class ShipItemData : ScriptableObject
{
    public string ItemID;
    public string itemName;
    public ShipItemType shipItemType;
    public int itemLevel = 1;
    public int itemTier = 1;
    public List<PlayerGolds> playerGolds;
    public List<ShipItemLevelData> shipItemLevelDatas;
    public List<ShipItemTierData> shipItemTierDatas;

    public ShipItemTierData GetShipItemTierData()
    {
        foreach (ShipItemTierData shipItemTier in this.shipItemTierDatas)
        {
            if (this.itemTier == shipItemTier.itemTier)
            {
                return shipItemTier;
            }
        }
        return null;
    }
    public ShipItemLevelData GetShipItemLevelData()
    {
        foreach (ShipItemLevelData shipItemLevel in this.shipItemLevelDatas)
        {
            if (this.itemTier == shipItemLevel.itemLevel)
            {
                return shipItemLevel;
            }
        }
        return null;
    }

    public int GetMaxTier()
    {
        return this.shipItemTierDatas.Count;
    }

    public int GetMaxLevel()
    {
        return this.itemTier * 5;
    }
    protected void OnEnable()
    {
       // this.LoadItems();
    }
    private void LoadItems()
    {
        if (shipItemLevelDatas == null || shipItemLevelDatas.Count == 0 || playerGolds == null || playerGolds.Count == 0)
        {
            return;  
        }

        int basePrice = playerGolds[0].Amount;  
        for (int tier = 1; tier < 2; tier++)
        {
            int tierPrice = basePrice * (tier + 1) - (basePrice * (tier + 1)) / 20;
            playerGolds.Add(new() {
                goldsData = playerGolds[0].goldsData,
                Amount = tierPrice
                
            });
           
        }
        ShipItemLevelData firstLevelData = shipItemLevelDatas[0];

        int maxLevels = GetMaxTier() * 5;
        for (int i = 1; i < maxLevels; i++)
        {
            ShipItemLevelData newLevelData = new ShipItemLevelData
            {
                itemLevel = i + 1,
                ShipItemAttributes = new(),
                Ingredients = new ()
            };

            foreach (var attribute in firstLevelData.ShipItemAttributes)
            {
                ShipItemAttributes newAttribute = new ShipItemAttributes
                {
                    Attribute = attribute.Attribute,
                    Amount = Mathf.FloorToInt(attribute.Amount * (1 + 0.1f * i))
                };
                newLevelData.ShipItemAttributes.Add(newAttribute);
            }

            newLevelData.Ingredients.Amount = firstLevelData.Ingredients.Amount;

            if (firstLevelData.Ingredients.Amount > 0)
            {
                newLevelData.Ingredients.Amount += 1000 * i;
            }

            shipItemLevelDatas.Add(newLevelData);
        }
    }
}



using UnityEngine;

[System.Serializable]
public class ShopEquipItemData
{
    public ShipItemData shipItemData;
    public bool IsPurchased = false;
    public int currentTier;
    public int currentLevel;

    public void SetState(ShipItemData shipItemData, bool IsPurchased) 
    {
        this.shipItemData = shipItemData;
        this.IsPurchased = IsPurchased;
    }
    public PlayerGolds GetGoldsData() 
    {
        return this.shipItemData.playerGolds[this.currentTier - 1];
    }
    public ShipItemTierData GetShipItemTierData()
    {
        foreach (ShipItemTierData shipItemTier in this.shipItemData.shipItemTierDatas)
        {
            if (this.currentTier == shipItemTier.itemTier)
            {
                return shipItemTier;
            }
        }
        return null;
    }

    public ShipItemLevelData GetShipItemLevelData()
    {
        foreach (ShipItemLevelData shipItemLevel in this.shipItemData.shipItemLevelDatas)
        {
            if (this.currentLevel == shipItemLevel.itemLevel)
            {
                return shipItemLevel;
            }
        }
        return null;
    }
    public int GetMaxLevel()
    {
        return this.currentTier * 5;
    }
}

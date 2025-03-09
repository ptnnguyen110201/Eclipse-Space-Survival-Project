using UnityEngine;

[System.Serializable]
public class ShipItemEquipData
{
    public ShipItemData shipItemData;
    public bool isEquipped = false;

    public int currentLevel;
    public int currentTier;

    public int baseHP;
    public int baseATK;

    public int boostedHP;
    public int boostedATK;

    public int currentHP;
    public int currentATK;

    public void SetState(ShipItemData shipItemData, bool isEquipped)
    {
        this.shipItemData = shipItemData;
        this.isEquipped = isEquipped;
    }

    public ShipItemTierData GetShipItemTierData()
    {
        foreach (ShipItemTierData shipItemTier in this.shipItemData.shipItemTierDatas)
        {
            if (currentTier == shipItemTier.itemTier)
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
            if (currentLevel == shipItemLevel.itemLevel)
            {
                return shipItemLevel;
            }
        }
        return null;
    }

    public int GetMaxLevel() => this.currentTier * 5;

    public bool GetMaxTier() => this.currentTier >= this.shipItemData.shipItemTierDatas.Count;

    public void CalculateBoostedAttributes()
    {
        ShipItemLevelData levelData = GetShipItemLevelData();
        ShipItemTierData tierData = GetShipItemTierData();

        if (levelData == null || tierData == null) return;

        this.baseHP = 0;
        this.baseATK = 0;

        foreach (ShipItemAttributes attribute in levelData.ShipItemAttributes)
        {
            switch (attribute.Attribute)
            {

                case ShipItemAttributesCode.HP:
                    if (this.shipItemData.shipItemType == ShipItemType.ShipHull)
                    {
                        this.baseHP = attribute.Amount;
                        this.boostedHP = 0;
                        break;
                    }
                    else
                    {
                        this.baseHP = attribute.Amount;
                        this.boostedHP = (int)((this.baseHP * tierData.itemtierBoostAmount) / 100.0f);
                        break;
                    }


                case ShipItemAttributesCode.ATK:

                    if (this.shipItemData.shipItemType == ShipItemType.ShipHull)
                    {
                        this.baseATK = attribute.Amount;
                        this.boostedATK = 0;
                        break;
                    }
                    else
                    {
                        this.baseATK = attribute.Amount;
                        this.boostedATK = (int)((this.baseATK * tierData.itemtierBoostAmount) / 100.0f);
                        break;
                    }

            }
        }

        this.currentHP = this.baseHP + this.boostedHP;
        this.currentATK = this.baseATK + this.boostedATK;
    }
}

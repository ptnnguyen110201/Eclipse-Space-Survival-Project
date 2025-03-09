using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Abilities/NewAbility")]
public class AbilityData : Selectable
{
    public BulletType bulletType = BulletType.None;
    public List<AbilityLevelData> levels;
    public AbilityLevelData UltimateLevel;


    public AbilityBuffData abilityBuffData;

    public AbilityLevelData GetAbilityWithBuff(int level, AbilityBuffLevelData abilityBuffLevelData)
    {
        AbilityLevelData baseLevelData = this.GetLevelData(level);
        if (abilityBuffLevelData == null) return baseLevelData;
        AbilityLevelData buffedData = new AbilityLevelData
        {
            level = baseLevelData.level,
            abilityAttributes = baseLevelData.ApplyBuff(abilityBuffLevelData)
        };

        return buffedData;
    }

    public AbilityLevelData GetLevelData(int level)
    {
        if (UltimateLevel.level == level) return UltimateLevel;
        foreach (AbilityLevelData data in levels)
            if (data.level == level) return data;
        return null;
    }

    public void SetImage(PlayerData playerData)
    {
        if (playerData == null) return;
        foreach (ShipItemEquipData equipData in playerData.shipItemEquipDatas.ShipItemEquiping)
        {
            if (equipData.shipItemData == null) continue;

            ShipItemType itemType = equipData.shipItemData.shipItemType;
            ShipItemTierData tierData = equipData.GetShipItemTierData();

            if (tierData == null) continue;

            if ((itemType == ShipItemType.ShipGun && this.bulletType == BulletType.RapidFire) ||
                (itemType == ShipItemType.ShipMissile && this.bulletType == BulletType.RPG))
            {
                this.itemSprite = tierData.itemTierSprite;
                break;
            }
        }
    }

}

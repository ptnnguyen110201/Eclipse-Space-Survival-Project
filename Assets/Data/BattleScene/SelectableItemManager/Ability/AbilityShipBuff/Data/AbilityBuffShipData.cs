using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newAbilityBuffShip", menuName = "Abilities/newAbilityBuffShip")]

public class AbilityBuffShipData : Selectable
{
    public List<AbilityBuffShipLevelData> levels;
    public AbilityBuffShipLevelData GetAbilityBuffShipLevel(int level)
    {
        foreach (AbilityBuffShipLevelData levelData in levels)
        {
            if (levelData.level == level)
                return levelData;
        }
        return null;
    }
}

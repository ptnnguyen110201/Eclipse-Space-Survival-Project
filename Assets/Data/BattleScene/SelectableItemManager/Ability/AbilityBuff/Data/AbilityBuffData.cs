using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewAbilityBuff", menuName = "Abilities/NewAbilityBuff")]

public class AbilityBuffData : Selectable
{
    public List<AbilityBuffLevelData> levels;
    public AbilityData abilityData;
    public AbilityBuffLevelData GetAbilityLevelData(int level)
    {
        foreach (AbilityBuffLevelData levelData in levels)
        {
            if (levelData.level == level)
                return levelData;
        }
        return null;
    }

}

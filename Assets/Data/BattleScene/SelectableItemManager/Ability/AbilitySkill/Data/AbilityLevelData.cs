using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AbilityLevelData
{
    public int level;
    public AbilityAttributes abilityAttributes;
    public List<string> Description;
    public AbilityAttributes ApplyBuff(AbilityBuffLevelData buffData)
    {
        if (buffData == null || buffData.abilityAttributes == null) return this.abilityAttributes;

        return new AbilityAttributes
        {
            ATK = abilityAttributes.ATK * (1 + buffData.abilityAttributes.ATK / 100),
            Cooldown = abilityAttributes.Cooldown * (1 - buffData.abilityAttributes.Cooldown / 100),
            SizeArea = abilityAttributes.SizeArea * (1 + buffData.abilityAttributes.SizeArea / 100),
            CritRate = abilityAttributes.CritRate + buffData.abilityAttributes.CritRate,
            CritDamage = abilityAttributes.CritDamage + buffData.abilityAttributes.CritDamage,
            ShotsPerAttack = abilityAttributes.ShotsPerAttack + buffData.abilityAttributes.ShotsPerAttack
        };
    }
    public string GetDescription()
    {
        if (Description == null || Description.Count == 0)
        {
            return "No description available.";
        }

        return string.Join("\n", Description);  
    }


}

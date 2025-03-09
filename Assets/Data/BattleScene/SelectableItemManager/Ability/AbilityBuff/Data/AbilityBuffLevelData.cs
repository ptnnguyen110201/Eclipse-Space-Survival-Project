using System;
using System.Collections.Generic;

[System.Serializable]
public class AbilityBuffLevelData
{
    public int level;
    public AbilityAttributes abilityAttributes;

    public string GetDescription()
    {
        List<string> descriptions = new List<string>();

        AddDescriptionIfPositiveOrNegative(descriptions, abilityAttributes.ATK, "ATK");
        AddDescriptionIfPositiveOrNegative(descriptions, abilityAttributes.Cooldown, "Cooldown", isNegative: true);
        AddDescriptionIfPositiveOrNegative(descriptions, abilityAttributes.SizeArea, "Size Area");
        AddDescriptionIfPositiveOrNegative(descriptions, abilityAttributes.ShotsPerAttack, "Shots/Attack");
        AddDescriptionIfPositiveOrNegative(descriptions, abilityAttributes.CritRate, "Crit Rate");
        AddDescriptionIfPositiveOrNegative(descriptions, abilityAttributes.CritDamage, "Crit Damage");

        return string.Join("\n", descriptions);
    }

    private void AddDescriptionIfPositiveOrNegative(List<string> descriptions, double value, string attributeName, bool isNegative = false)
    {
        if (value > 0)
        {
            string prefix = isNegative ? "-" : "+";
            descriptions.Add($"{attributeName} {prefix}{Math.Round(value)}%");
        }
    }
}

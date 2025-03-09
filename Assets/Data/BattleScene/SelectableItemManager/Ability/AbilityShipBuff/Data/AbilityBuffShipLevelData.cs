using System.Buffers.Text;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityBuffShipLevelData
{
    public int level;
    public AbilityBuffShipType abilityBuffShipType;
    public int Amount;

    public void ApplyBuff(ShipAttributes baseAttributes, ShipAttributes buffedAttributes)
    {
        switch (this.abilityBuffShipType)
        {
            case AbilityBuffShipType.HP:
                buffedAttributes.HP = baseAttributes.HP * (1 + this.Amount / 100.0);
                break;

            case AbilityBuffShipType.ATK:
                buffedAttributes.ATK = baseAttributes.ATK * (1 + this.Amount / 100.0);
                break;

            default:
                Debug.LogWarning("Unknown buff type. No buff applied.");
                break;
        }
    }
    public string GetDescription()
    {
        if (Amount <= 0)
        {
            return "No buff applied.";
        }

        return abilityBuffShipType switch
        {
            AbilityBuffShipType.HP => $"Increases HP by {Amount}%",
            AbilityBuffShipType.ATK => $"Increases ATK by {Amount}%",
            _ => "Unknown buff type."
        };
    }
}

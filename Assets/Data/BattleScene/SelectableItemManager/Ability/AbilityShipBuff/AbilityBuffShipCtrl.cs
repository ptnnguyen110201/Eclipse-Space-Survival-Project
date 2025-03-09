using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AbilityBuffShipCtrl : AbilityCtrl
{
    [SerializeField] protected AbilityBuffShipLevelData abilityBuffShipLevel;
    protected override void LoadComponents()
    {
        base.LoadComponents();

    }
    public AbilityBuffShipLevelData GetAbilityBuffLevelData()
    {
        int currentLevel = this.GetAbilityLevel().GetCurrentLevel();
        AbilityBuffShipLevelData abilityBuffShipLevel =  this.GetAbilityBuffShipData().GetAbilityBuffShipLevel(currentLevel);
        return this.abilityBuffShipLevel = abilityBuffShipLevel;
    }

    public AbilityBuffShipData GetAbilityBuffShipData() => this.selectable as AbilityBuffShipData;
}

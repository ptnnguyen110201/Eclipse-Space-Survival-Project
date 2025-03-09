using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBuffCtrl : AbilityCtrl
{
    [SerializeField] protected AbilityBuffLevelData abilityBuffLevelData;
    protected override void LoadComponents()
    {
        base.LoadComponents();

    }
    public AbilityBuffLevelData GetAbilityBuffLevelData()
    {
        int currentLevel = this.GetAbilityLevel().GetCurrentLevel();
        AbilityBuffLevelData abilityBuffLevelData =  this.GetAbilityBuffData().GetAbilityLevelData(currentLevel);
        return this.abilityBuffLevelData  = abilityBuffLevelData;
    }

    public AbilityBuffData GetAbilityBuffData() => this.selectable as AbilityBuffData;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySkillCtrl : AbilityCtrl
{
    [SerializeField] protected AbilityLevelData abilityLevelData;
    [SerializeField] protected AbilityShoot abilityShoot;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityShoot();

    }
    public void SetAbilityLevelData(AbilityBuffLevelData abilityBuffLevelData)
    {
        int level = this.abilityLevel.GetCurrentLevel();
        AbilityLevelData buffedData = this.GetAbilityData().GetAbilityWithBuff(level, abilityBuffLevelData);
        if (buffedData != null)
        {
            ShipAttributes shipAttributes = ShipManager.Instance.GetShipCtrl().GetShipAttributes();
            AbilityLevelData finalAttribute = new AbilityLevelData
            {
                abilityAttributes = new AbilityAttributes
                {
                    ATK = shipAttributes.ATK * (1 + buffedData.abilityAttributes.ATK / 100),
                    Cooldown = buffedData.abilityAttributes.Cooldown,
                    SizeArea = buffedData.abilityAttributes.SizeArea,
                    ShotsPerAttack = buffedData.abilityAttributes.ShotsPerAttack,
                    CritRate = buffedData.abilityAttributes.CritRate,
                    CritDamage = buffedData.abilityAttributes.CritDamage
                }
            };
            this.abilityLevelData = finalAttribute;
        }
    }

    protected virtual void LoadAbilityShoot()
    {
        if (this.abilityShoot != null) return;
        this.abilityShoot = transform.GetComponentInChildren<AbilityShoot>();
        Debug.Log(transform.name + "Load AbilityShoot", gameObject);
    }
    public AbilityLevelData GetAbilityLevelData() => this.abilityLevelData;
    public AbilityData GetAbilityData() => this.selectable as AbilityData;
}

using UnityEngine;

public class AbilitySkillAbstract : AbilityAbstract
{
    public AbilitySkillCtrl GetShipAbilityCtrl() => this.GetAbilityCtrl() as AbilitySkillCtrl;
}

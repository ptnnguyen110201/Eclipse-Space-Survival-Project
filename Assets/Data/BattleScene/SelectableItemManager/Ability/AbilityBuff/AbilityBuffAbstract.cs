using UnityEngine;

public class AbilityBuffAbstract : AbilityAbstract
{
   
    public AbilityBuffCtrl GetAbilityBuffCtrl() => this.GetAbilityCtrl() as AbilityBuffCtrl;
}

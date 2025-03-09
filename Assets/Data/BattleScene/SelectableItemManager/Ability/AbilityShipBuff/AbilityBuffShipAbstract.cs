using UnityEngine;

public class AbilityBuffShipAbstract : AbilityAbstract
{
   
    public AbilityBuffShipCtrl GetAbilityBuffShipCtrl() => this.GetAbilityCtrl() as AbilityBuffShipCtrl;
}

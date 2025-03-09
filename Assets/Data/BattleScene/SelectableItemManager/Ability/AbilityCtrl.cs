using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCtrl : SelectableCtrl
{
    [SerializeField] protected ShipAbilityCtrl shipAbilityCtrl;
    [SerializeField] protected AbilityLevel abilityLevel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityManagerCtrl();
        this.LoadAbilityLevel();
    }
    protected virtual void LoadAbilityManagerCtrl()
    {
        if (this.shipAbilityCtrl != null) return;
        this.shipAbilityCtrl = transform.parent.GetComponent<ShipAbilityCtrl>();
        Debug.Log(transform.name + "Load AbilityManagerCtrl ", gameObject);
    }
    protected virtual void LoadAbilityLevel()
    {
        if (this.abilityLevel != null) return;
        this.abilityLevel = transform.GetComponentInChildren<AbilityLevel>();
        Debug.Log(transform.name + "Load AbilityLevel ", gameObject);
    }

    public ShipAbilityCtrl GetShipAbilityCtrl() => this.shipAbilityCtrl;
    public AbilityLevel GetAbilityLevel() => this.abilityLevel;

}

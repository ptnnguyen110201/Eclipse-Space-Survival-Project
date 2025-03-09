using UnityEngine;

public abstract class AbilityAbstract : FuncManager
{
    [SerializeField] protected AbilityCtrl abilityCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAbilityCtrl();
    }
    protected virtual void LoadAbilityCtrl()
    {
        if (this.abilityCtrl != null) return;
        this.abilityCtrl = transform.parent.GetComponent<AbilityCtrl>();
        Debug.Log(transform.name + ": LoadAbilityCtrl", gameObject);
    }
    public AbilityCtrl GetAbilityCtrl() => this.abilityCtrl;
}

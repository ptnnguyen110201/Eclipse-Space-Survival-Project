using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableCtrl : FuncManager
{
    [SerializeField] protected Selectable selectable;
    protected override void LoadComponents()
    {
        base.LoadComponents(); 
        this.LoadSelectAble();
    } 
    protected virtual void LoadSelectAble() 
    {
        if (this.selectable != null) return;
        string resPath = "SelectableItem/Ability/" + transform.name;
        this.selectable = Resources.Load<Selectable>(resPath);
        Debug.LogWarning(transform.name + ": Load ShipSO" + resPath);
    }
    public Selectable GetSelectable() => this.selectable;
}

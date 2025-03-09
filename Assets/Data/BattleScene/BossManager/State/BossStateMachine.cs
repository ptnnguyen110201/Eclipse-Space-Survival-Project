using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    [SerializeField] protected BossCtrl bossCtrl;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossCtrl();
    }

    protected virtual void LoadBossCtrl() 
    {
        if (this.bossCtrl != null) return;
        this.bossCtrl = transform.parent.GetComponent<BossCtrl>();
        Debug.Log(transform.name + "LoadBossCtrl ", gameObject);
    }
    public BossCtrl GetBossCtrl() => this.bossCtrl;
}

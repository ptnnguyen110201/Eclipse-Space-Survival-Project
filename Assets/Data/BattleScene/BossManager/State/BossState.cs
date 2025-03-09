using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : State
{

    [SerializeField] protected float StateTimer;
    [SerializeField] protected List<BossAbility> bossAbilities;
    [SerializeField] protected BossAbility currentAbility;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossAbility();
    }
  
    protected virtual void LoadBossAbility()
    {

        if (this.bossAbilities.Count > 0) return;
        foreach (Transform obj in this.transform)
        {
            BossAbility bossAbility = obj.GetComponent<BossAbility>();
            if (bossAbility == null) continue;
            this.bossAbilities.Add(bossAbility);

        }
    }


    public BossStateMachine GetBossStateMachine()
    {
        BossStateMachine stateMachine = this.stateMachine as BossStateMachine;
        return stateMachine;
    }

}

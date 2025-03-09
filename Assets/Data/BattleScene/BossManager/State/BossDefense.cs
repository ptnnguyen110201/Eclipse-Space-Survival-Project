using System.Collections;
using UnityEngine;

public class BossDefenseState : BossState
{


    public override IEnumerator Enter()
    {
        yield return this.StartCoroutine(this.Deffensing());
        yield return Exit();
    }
    protected virtual IEnumerator Deffensing()
    {
        int randomIndex = Random.Range(0, this.bossAbilities.Count);
        Transform bossTrans = this.GetBossStateMachine().GetBossCtrl().transform;
        this.currentAbility = this.bossAbilities[randomIndex];
        yield return this.StartCoroutine(this.currentAbility.Execute(bossTrans, this.GetBossStateMachine(), this));
    }
    public override IEnumerator Exit()
    {
        yield return new WaitForSeconds(1f);
        this.stateMachine.ResetState();
    }
}

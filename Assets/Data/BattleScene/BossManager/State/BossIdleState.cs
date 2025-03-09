using System.Collections;
using UnityEngine;

public class BossIdleState : BossState
{
    public override IEnumerator Enter()
    {
        Rigidbody2D rb = this.GetBossStateMachine().GetBossCtrl().GetRigidbody2D();
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(this.StateTimer);
        yield return Exit();
    }

    public override IEnumerator Exit()
    {
        this.stateMachine.ChangeToNextState();
        yield break;
    }
}

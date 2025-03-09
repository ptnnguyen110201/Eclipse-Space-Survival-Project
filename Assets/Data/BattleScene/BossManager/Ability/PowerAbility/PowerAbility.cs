using System.Collections;
using UnityEngine;

public class PowerAbility : BossAbility
{

    public override IEnumerator Execute(Transform bossTransform, BossStateMachine bossStateMachine, BossState bossState)
    {
        if (!this.CanUse()) yield break;
        for (int i = 0; i < 3; i++)
        {
            this.SpawnHealingEffect(bossTransform);
            this.PowerSend(bossStateMachine);

            yield return new WaitForSeconds(0.5f);
        }
        this.UpdateLastUsedTime();
    }
    private void PowerSend(BossStateMachine bossStateMachine)
    {
        Transform PlayerPos = this.PlayerPos;
        DamageSender damageSender = bossStateMachine.GetBossCtrl().GetDamageSender();
        if (damageSender == null) return;
        damageSender.Send(PlayerPos);
    }
    private void SpawnHealingEffect(Transform bossTransform)
    {
        Transform newFx = FxSpawner.Instance.SpawnFx(FxType.Boom, transform.position, transform.rotation);
        newFx.gameObject.SetActive(true);
    }

}

using System.Collections;
using UnityEngine;

public class HealthAbility : BossAbility
{
    [SerializeField] protected float healthPercent = 0.1f;
    [SerializeField] protected float healthDuration = 5f;

    [SerializeField] protected Transform healhEffect = null;

    public override IEnumerator Execute(Transform bossTransform, BossStateMachine bossStateMachine, BossState bossState)
    {
        if(!this.CanUse()) yield break;
        this.SpawnHealingEffect(bossTransform);
        this.UpdateLastUsedTime();
        yield return this.RegenerateHealthSmoothly(bossStateMachine);
        this.ResetHealingEffect();
    }

    private void SpawnHealingEffect(Transform bossTransform)
    {
        if (this.healhEffect == null)
        {
            this.healhEffect = FxSpawner.Instance.SpawnFx(FxType.BossHealth, bossTransform.position, bossTransform.rotation);
        }
        this.healhEffect.gameObject.SetActive(true);
    }

    private IEnumerator RegenerateHealthSmoothly(BossStateMachine bossStateMachine)
    {
        var damageReceiver = bossStateMachine.GetBossCtrl().GetDamageReceiver();
        if (damageReceiver == null) yield break;

        double maxHealth = damageReceiver.HPMax;
        double totalHealthToRestore = maxHealth * this.healthPercent;
        double healthRestored = 0;

        float elapsedTime = 0f;
        float updateInterval = 0.1f;
        double healthPerUpdate = totalHealthToRestore / (this.healthDuration / updateInterval);

        while (elapsedTime < this.healthDuration)
        {
            if (healthRestored < totalHealthToRestore)
            {
                double healthToAdd = Mathf.Min((float)healthPerUpdate, (float)(totalHealthToRestore - healthRestored));
                damageReceiver.Add(healthToAdd);
                healthRestored += healthToAdd;
            }

            elapsedTime += updateInterval;
            yield return new WaitForSeconds(updateInterval);
        }
    }

    private void ResetHealingEffect()
    {
        if (this.healhEffect == null) return;
        FxSpawner.Instance.Despawn(this.healhEffect);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossState
{
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected List<BossAbility> unusedAbilities;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShootPoint();
    }

    protected virtual void LoadShootPoint()
    {
        if (this.shootPoint != null) return;
        this.shootPoint = transform.GetComponentInChildren<Transform>();
        Debug.Log(transform.name + "Load ShootPoint ", gameObject);
    }

    public override IEnumerator Enter()
    {
        this.InitializeAbilities();
        yield return this.StartCoroutine(this.FlashAngerEffect());

        while (this.unusedAbilities.Count > 0)
        {
            yield return this.StartCoroutine(this.Attacking());
        }

        yield return this.Exit();
    }

    protected virtual void InitializeAbilities()
    {
        if (this.bossAbilities.Count <= 0) return;
        this.unusedAbilities = new List<BossAbility>(this.bossAbilities);
    }

    protected virtual IEnumerator Attacking()
    {
        int randomIndex = Random.Range(0, this.unusedAbilities.Count);
        this.currentAbility = unusedAbilities[randomIndex];
        this.unusedAbilities.RemoveAt(randomIndex);
        yield return this.StartCoroutine(this.currentAbility.Execute(this.shootPoint, this.GetBossStateMachine(), this));
    }

    protected IEnumerator FlashAngerEffect()
    {
        float elapsedTime = 0f;
        SpriteRenderer spriteRenderer = this.GetBossStateMachine().GetBossCtrl().GetObjectModel().GetSpriteRenderer();
        Color originalColor = spriteRenderer.color;
        Color angryColor = Color.red;

        while (elapsedTime < 2f)
        {
            spriteRenderer.color = angryColor;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);

            elapsedTime += 0.2f;
        }
        spriteRenderer.color = originalColor;
    }

    public override IEnumerator Exit()
    {
        yield return new WaitForSeconds(1f);
        this.stateMachine.ResetState();
    }
}

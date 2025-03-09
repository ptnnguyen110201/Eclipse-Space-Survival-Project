using DG.Tweening;
using UnityEngine;

public class EnemyMove : ObjectMove
{
    [SerializeField] protected float tweenDuration = 0.2f;
    [SerializeField] protected Tweener moveTweener;
    public override void Move()
    {
        throw new System.NotImplementedException();
    }


    public void MoveTowardsPlayer(Transform playerPos)
    {
        if (playerPos == null) return;

        Rigidbody2D rb = this.objectCtrl.GetRigidbody2D();
        Vector2 targetDirection = (playerPos.position - rb.transform.position).normalized;
        this.moveTweener?.Kill();
        moveTweener = DOTween.To(
            () => rb.velocity,
            x => rb.velocity = x,
            targetDirection * this.Speed,
            tweenDuration
        )
        .SetEase(Ease.Linear)
        .SetUpdate(UpdateType.Fixed);
    }

    protected override void OnDisable()
    {
        this.moveTweener?.Kill();
        base.OnDisable();

    }

}

using UnityEngine;
using DG.Tweening;

public class EnergyBallImpact : BulletImpart
{
    protected override void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.contacts.Length == 0)
        {
            Debug.LogError("No contact points available in collision.");
            return;
        }

        Vector3 currentDirection = this.BulletCtrl.GetMoveBase().GetDirection();
        Vector3 normal = collision2D.contacts[0].normal;
        Vector3 newDirection = Vector2.Reflect(currentDirection, normal);

        DOTween.To(() => currentDirection, dir => this.BulletCtrl.GetMoveBase().SetDirection(dir), newDirection, 0.1f)
            .SetEase(Ease.OutQuad);
        base.OnCollisionEnter2D(collision2D);
    }
}

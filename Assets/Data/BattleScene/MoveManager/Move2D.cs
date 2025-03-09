using UnityEngine;
using DG.Tweening;

public class Move2D : MoveBase
{
    public override void Move()
    {
       this.Moving();
    }

    public virtual void Moving()
    {
        KillTween(); // Ensure no conflicting tweens are running

        // Tween the Rigidbody's velocity to smoothly transition to the new direction and speed
        movementTween = DOTween.To(
            () => this.Rigidbody.velocity,                   // Current velocity getter
            v => this.Rigidbody.velocity = v,               // Set the Rigidbody's velocity
            (Vector2)this.Direction.normalized * this.Speed, // Target velocity as Vector2
                        0f                            // Duration for the transition
        ).SetEase(Ease.Linear);                             // Use linear easing for consistent speed
    }
}

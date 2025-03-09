using UnityEngine;

public class OrbitalImpact : BulletImpart
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }

    protected override void OnCollisionEnter2D(Collision2D collision2D)
    {
        base.OnCollisionEnter2D(collision2D);
    }
}

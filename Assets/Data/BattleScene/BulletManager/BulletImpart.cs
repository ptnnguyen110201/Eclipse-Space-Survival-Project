using UnityEngine;

public class BulletImpart : BulletAbstract
{
    [Header("Object Impart")]
    [SerializeField] protected Collider2D Collider2D;
    [SerializeField] protected bool useTrigger = true;

    private Transform shooter;
    private SingleDamageSender singleDamageSender;
    private AOEDamageSender aoeDamageSender;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.shooter = this.BulletCtrl.GetShooter();
        this.singleDamageSender = this.BulletCtrl.GetDamageSender() as SingleDamageSender;
        this.aoeDamageSender = this.BulletCtrl.GetDamageSender() as AOEDamageSender;
    }

    protected virtual void LoadCollider()
    {
        if (this.Collider2D != null) return;
        this.Collider2D = GetComponent<Collider2D>();
        this.useTrigger = this.Collider2D.isTrigger;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (!this.useTrigger) return;
        if (collider2D.transform.parent == shooter) return;

        if (singleDamageSender != null)
        {
            singleDamageSender.Send(collider2D.transform);
            return;
        }

        this.DestroyBullet();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (this.useTrigger) return;
        if (collision2D.transform.parent == shooter) return;

        if (singleDamageSender != null)
        {
            singleDamageSender.Send(collision2D.transform);
            return;
        }

        this.DestroyBullet();
    }

    public virtual void DestroyBullet()
    {
        if (aoeDamageSender != null)
        {
            aoeDamageSender.DestroyObject();
        }
    }
}

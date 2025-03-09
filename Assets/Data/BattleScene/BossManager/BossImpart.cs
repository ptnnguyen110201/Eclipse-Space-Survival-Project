
using UnityEngine;

public class BossImpart : BossAbtracts
{
    [Header("Object Impart")]
    [SerializeField] protected Collider2D Collider2D;
    [SerializeField] protected float ImpartTime = 0.1f;
    [SerializeField] protected float ImpartTimer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }

    protected virtual void LoadCollider()
    {
        if (this.Collider2D != null) return;
        this.Collider2D = transform.GetComponent<Collider2D>();
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }


    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Player")) return;
        this.ImpartTimer = this.ImpartTime;

    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Player")) return;
        this.ImpartTimer += Time.fixedDeltaTime;
        if (this.ImpartTimer < this.ImpartTime) return;

        this.ImpartTimer = 0;
        this.bossCtrl.GetDamageSender().Send(collision.transform);
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Player")) return;
        this.ImpartTimer = 0;

    }

}




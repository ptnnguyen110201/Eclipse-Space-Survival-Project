
using UnityEngine;

public class SingularityImpact : BulletImpart
{
    [SerializeField] protected float ImpartTime = 0.5f;
    [SerializeField] protected float ImpartTimer;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) return;
        this.ImpartTimer = this.ImpartTime;
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) return;
        this.ImpartTimer += Time.fixedDeltaTime;
        if (this.ImpartTimer < this.ImpartTime) return;
        this.ImpartTimer = 0;

        SingleDamageSender singleDamageSender = this.BulletCtrl.GetDamageSender() as SingleDamageSender;
        if (singleDamageSender != null)
        {
            singleDamageSender.Send(collision.transform);
        }
        
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) return;
        this.ImpartTimer = 0;

    }
   
}




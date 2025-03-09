

using System.Collections.Generic;
using UnityEngine;


public class BulletDespawnByTime : DespawnByTime
{
    [SerializeField] protected BulletCtrl bulletCtrl;
    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = 2f;
    }
    public override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);

    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }

    protected virtual void LoadBulletCtrl()
    {
        if (this.bulletCtrl != null) return;
        this.bulletCtrl = transform.parent.GetComponent<BulletCtrl>();
        Debug.Log(transform.name + ": LoadBulletCtrl", gameObject);
    }

    protected virtual void SendDamageByDestroy() 
    {
        this.bulletCtrl.GetBulletImpart().DestroyBullet();
    }


}

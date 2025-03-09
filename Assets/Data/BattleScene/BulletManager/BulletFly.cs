using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : Move2D
{
    [SerializeField] protected BulletCtrl BulletCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }
    public override void Move()
    {
        base.Move();
    }

    protected virtual void LoadBulletCtrl()
    {
        if (this.BulletCtrl != null) return;
        this.BulletCtrl = transform.parent.GetComponent<BulletCtrl>();
        Debug.Log(transform.name + ": LoadBulletCtrl", gameObject);
    }

    public BulletCtrl GetBulletCtrl() => this.BulletCtrl;

}

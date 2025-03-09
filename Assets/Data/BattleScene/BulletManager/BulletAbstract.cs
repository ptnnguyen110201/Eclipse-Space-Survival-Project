using UnityEngine;

public abstract class BulletAbstract : FuncManager
{
    [Header("Bullet Abtract")]
    [SerializeField] protected BulletCtrl BulletCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }

    protected virtual void LoadBulletCtrl()
    {
        if (this.BulletCtrl != null) return;
        this.BulletCtrl = transform.parent.GetComponent<BulletCtrl>();
        Debug.Log(transform.name + ": LoadBulletCtrl", gameObject);
    }

    public BulletCtrl GetBulletCtrl() => this.BulletCtrl;
}

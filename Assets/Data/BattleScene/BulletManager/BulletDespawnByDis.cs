

using UnityEngine;

public class BulletDespawnByDis : DespawnByDistance
{
    [SerializeField] protected BulletCtrl bulletCtrl;
    public override void DespawnObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.disLimit = 15;
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


}

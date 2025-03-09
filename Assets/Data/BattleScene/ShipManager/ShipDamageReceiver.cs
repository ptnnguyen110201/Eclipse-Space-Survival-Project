using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShipDamageReceiver : DamageReceiver
{
    [SerializeField] protected ShipCtrl shipCtrl;
    [SerializeField] protected ShipHPBar shipHPBar;
    protected override void OnEnable()
    {
        this.StartCoroutine(this.SetHP());
        base.OnEnable();
        this.shipHPBar.SetCurrentValue(this.hp);
        this.shipHPBar.SetMaxValue(this.hpMax);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.StopCoroutine(this.SetHP());
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipCtrl();
    }
    protected virtual void LoadShipCtrl()
    {
        if (this.shipCtrl != null) return;
        this.shipCtrl = transform.parent.GetComponent<ShipCtrl>();
        this.shipHPBar = transform.GetComponentInChildren<ShipHPBar>();
        Debug.LogWarning(transform.name + ": LoadShipCtrl", gameObject);
    }
    protected IEnumerator SetHP()
    {
        while (true)
        {
            double hpMax = this.shipCtrl.GetShipAttributes().HP;
            this.hpMax = hpMax;
            yield return null;
        }
    }

    protected override void OnDead()
    {
        this.DespawnBulletOnDead();
        ShipManager.Instance.GetShipSpawner().Despawn(transform.parent);
        ShipManager.Instance.NotifyShipDead();



    }
    public override void Deduct(double deduct)
    {
        base.Deduct(deduct);
        this.shipHPBar.SetCurrentValue(this.hp);
        this.shipHPBar.SetMaxValue(this.hpMax);

    }
    public override void Add(double add)
    {
        base.Add(add);
        this.shipHPBar.SetCurrentValue(this.hp);
        this.shipHPBar.SetMaxValue(this.hpMax);
    }
    private void DespawnBulletOnDead()
    {
        List<BulletCtrl> bulletCtrls = new List<BulletCtrl>(BulletManager.Instance.bulletCtrls);
        List<BulletCtrl> bossBullet = new List<BulletCtrl>();
        foreach (BulletCtrl bullet in bulletCtrls)
        {
            if (bullet.GetShooter() == transform.parent)
            {
                bossBullet.Add(bullet);
            }
        }
        foreach (BulletCtrl bullet in bossBullet)
        {
            BulletSpawner.Instance.Despawn(bullet.transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageReceiver : ObjectDamageReceiver
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDead()
    {
         base.OnDead();
        BossCtrl bossCtrl = this.objectCtrl as BossCtrl;

        BossHPBarUI.Instance.HideBossHPBar();       
        this.FxSpawn(bossCtrl.GetBossData().fxType);
        this.DespawnBulletOnDead(); 
        bossCtrl.HandleDeath();
     



    }
    private void FxSpawn(FxType fxType)
    {
        Transform newFx = FxSpawner.Instance.SpawnFx(fxType, transform.parent.position, Quaternion.identity);
        newFx.gameObject.SetActive(true);
    }
    public override void Deduct(double deduct)
    {
        base.Deduct(deduct);
        BossCtrl bossCtrl = this.objectCtrl as BossCtrl;
        bossCtrl.UpdateHealth(this.hp, this.hpMax);


    }
    public override void Add(double add)
    {
        base.Add(add);
        BossCtrl bossCtrl = this.objectCtrl as BossCtrl;
        bossCtrl.UpdateHealth(this.hp, this.hpMax);



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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityShoot : AbilitySkillAbstract
{

    [SerializeField] protected Transform ShootPoint;
    [SerializeField] protected AbilityAttributes abilityAttributes;
    [SerializeField] protected float ShootInterval = 0.1f;
    [SerializeField] protected bool isReadyToShoot = true;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShootPoint();
    }

    protected virtual void LoadShootPoint()
    {
        if (this.ShootPoint != null) return;
        this.ShootPoint = transform.Find("shootPoint").GetComponent<Transform>();
        Debug.Log(transform.name + ": LoadShootPoint", gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.StartShooting();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.StopShooting();
    }
    protected virtual void StartShooting()
    {
        this.isReadyToShoot = true;
        this.StartCoroutine(this.SetAbilityAttribute());

        this.StartCoroutine(this.Shooting());
    }
    protected virtual void StopShooting() 
    {
       
        this.StopCoroutine(this.SetAbilityAttribute());
        this.StopCoroutine(this.Shooting());
        this.isReadyToShoot = false;
    }
    private IEnumerator Shooting()
    {
        while (true)
        {
            if (this.IsCanShoot() && this.isReadyToShoot)
            {
                yield return this.ShootType();
            }
            yield return null;
        }
    }



    private bool IsCanShoot()
    {
        return this.ObjShot() != null;
    }

    private IEnumerator SetAbilityAttribute()
    {
        while (true)
        {
            AbilityAttributes newAttributes = this.GetShipAbilityCtrl()?.GetAbilityLevelData()?.abilityAttributes;
            if (newAttributes == null)
            {
                yield return new WaitForSeconds(0.1f); 
                continue;
            }

            this.abilityAttributes = newAttributes;
            yield return new WaitForSeconds(0.1f);
        }
    }




    protected virtual IEnumerator ShootType()
    {
        this.isReadyToShoot = false;
        yield return new WaitForSeconds(this.abilityAttributes.Cooldown);
        this.isReadyToShoot = true;
    }
    protected virtual Transform ObjShot()
    {
        Transform nearEnemy =  ShipManager.Instance.GetShipCtrl().GetShipArea().GetNearestEnemy();
        return nearEnemy;
    }

    
    protected virtual Transform BulletSpawn(SoundCode soundCode, Vector3 Pos, Quaternion Rot)
    {
        AbilitySkillCtrl shipAbilityCtrl = this.abilityCtrl as AbilitySkillCtrl;
        BulletType bulletType = shipAbilityCtrl.GetAbilityData().bulletType;
        Transform obj = BulletSpawner.Instance.SpawnBullet(bulletType, Pos, Rot);
        BulletCtrl newBullet = obj.GetComponent<BulletCtrl>();
        newBullet.SetBullet(this.abilityAttributes, shipAbilityCtrl.GetAbilityData().itemSprite, ShipManager.Instance.GetShipCtrl().transform);
        BulletManager.Instance.AddBullet(newBullet);
        AudioManager.Instance.SpawnSFX(soundCode);
        return obj;
    }

  
}

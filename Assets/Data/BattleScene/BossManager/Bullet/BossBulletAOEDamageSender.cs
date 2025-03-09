using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletAOEDamageSender : AOEDamageSender
{

    public override void Send(DamageReceiver damageReceiver)
    {
        Vector3 hitPos = damageReceiver.transform.parent.position;
        bool isCriticalDamage = this.IsCriticalDamage();
        double finalDamage = this.CalculateDamage(isCriticalDamage);
        this.FxPopup(finalDamage, isCriticalDamage, hitPos);
        base.Send(damageReceiver);
    }
    protected virtual void FxPopup(double finalDamage, bool isCritATK, Vector3 hitPos)
    {
        Transform FxDamage = FxSpawner.Instance.SpawnFx(FxType.PopupDamage, hitPos, Quaternion.identity);
        FxDamagePopup TextDamage = FxDamage.GetComponent<FxDamagePopup>();
        TextDamage.SetDamage(finalDamage, isCritATK, this.CritATK);
        TextDamage.gameObject.SetActive(true);

    }
    public override void TriggerAOEDamage()
    {
        List<Transform> objs = this.FindTargetsInRange();
        foreach (Transform obj in objs)
        {
            if (obj == null) continue;
            if (obj.tag == "Boss" || obj.tag == "Enemy") continue;
            this.Send(obj);
        }
    }
    protected virtual void FxBullet(FxType fxType)
    {
        Transform newFx = FxSpawner.Instance.SpawnFx(fxType, transform.position, Quaternion.identity);
        if (this.SizeArea >= 1)
        {
            newFx.transform.localScale = new Vector3(this.SizeArea, this.SizeArea, 0f);
        }
        newFx.gameObject.SetActive(true);
    }
    public override void DestroyObject()
    {
        this.FxBullet(FxType.None);
        base.DestroyObject();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSingleDamageSender : SingleDamageSender
{
    public override void Send(DamageReceiver damageReceiver)
    {
        Vector3 hitPos = damageReceiver.transform.parent.position;
        bool isCriticalDamage = this.IsCriticalDamage();
        double finalDamage = this.CalculateDamage(isCriticalDamage);
        this.FxPopup(finalDamage, isCriticalDamage, hitPos);
        this.FxBullet(FxType.None);
        base.Send(damageReceiver);
    }
    protected virtual void FxPopup(double finalDamage, bool isCritATK, Vector3 hitPos)
    {
        Transform FxDamage = FxSpawner.Instance.SpawnFx(FxType.PopupDamage, hitPos, Quaternion.identity);
        FxDamagePopup TextDamage = FxDamage.GetComponent<FxDamagePopup>();
        TextDamage.SetDamage(finalDamage, isCritATK, this.CritATK);
        TextDamage.gameObject.SetActive(true);
    }
    protected virtual void FxBullet(FxType fxType)
    {
        if (fxType == FxType.None) return;
        Transform newFx = FxSpawner.Instance.SpawnFx(fxType, transform.position, Quaternion.identity);
        if(this.SizeArea >= 1) 
        {
            newFx.transform.localScale = new Vector3(this.SizeArea, this.SizeArea, 0f);
        }
        newFx.gameObject.SetActive(true);
    }
}

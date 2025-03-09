using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageSender : FuncManager
{
    [SerializeField] protected double ATK;
    [SerializeField] protected float SizeArea;
    [SerializeField] protected float CritRate;
    [SerializeField] protected float CritATK;
    public virtual void Send(Transform obj)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
    }

    public virtual void Send(DamageReceiver damageReceiver) => damageReceiver.Deduct(this.ATK);
    public virtual void SetATK(double ATK) => this.ATK = ATK;
    public virtual void SetSizeArea(float SizeArea) => this.SizeArea = SizeArea;
    public virtual void SetCritRate(float CritRate) => this.CritRate = CritRate;
    public virtual void SetCritDamage(float CritATK) => this.CritATK = CritATK;

}

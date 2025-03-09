using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSendBase : DamageSender
{
    public virtual void DestroyObject() 
    {
        // for override 
    } 

    protected virtual double CalculateDamage(bool isCritical)
    {
        if (!isCritical) return this.ATK;
        return this.ATK * this.CritATK;
    }
  
    protected virtual bool IsCriticalDamage() => Random.Range(0f, 1f) < this.CritRate;


}
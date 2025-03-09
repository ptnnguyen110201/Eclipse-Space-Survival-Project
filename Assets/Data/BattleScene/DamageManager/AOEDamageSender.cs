using System.Collections.Generic;
using UnityEngine;

public class AOEDamageSender : DamageSendBase
{
    public virtual void TriggerAOEDamage()
    {
      //
    }

    protected virtual List<Transform> FindTargetsInRange()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.parent.position, this.SizeArea);
        List<Transform> targets = new();
        foreach (Collider2D collider in hitTargets)
        {
            DamageReceiver damageReceiver = collider.GetComponentInChildren<DamageReceiver>();
            if (damageReceiver == null) continue;
            targets.Add(collider.transform);
    
        }
        return targets;
    }
    public override void DestroyObject()
    {
        this.TriggerAOEDamage();
        base.DestroyObject();
    }
}

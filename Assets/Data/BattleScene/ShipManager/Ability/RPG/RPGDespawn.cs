

using System.Collections.Generic;
using UnityEngine;


public class RocketBulletDespawn : BulletDespawnByTime
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = 1.5f;
    }
    public override void DespawnObject()
    {
        base.SendDamageByDestroy();
        BulletSpawner.Instance.Despawn(transform.parent);
      
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletCtrl();
    }

    


}

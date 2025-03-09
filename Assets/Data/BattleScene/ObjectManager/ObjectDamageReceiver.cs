using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamageReceiver : DamageReceiver
{
    [Header("Object DamageReceiver")]
    [SerializeField] protected ObjectCtrl objectCtrl;
    protected override void OnEnable()
    {
        this.SetHP();
        base.OnEnable();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }
    protected virtual void LoadCtrl()
    {
        if (this.objectCtrl != null) return;
        this.objectCtrl = transform.parent.GetComponent<ObjectCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }
    protected virtual void SetHP() 
    {
        ObjectAttribute objectAttribute = this.objectCtrl.GetObjectAttribute();
        if (objectAttribute == null)
        {
            Debug.Log("ObjectAttribute is Null");
            return; 
        }
        this.hpMax = objectAttribute.objectHp;
    }
    protected override void OnDead()
    {
        this.objectCtrl.GetDespawner().DespawnObject();
        this.OnDeadDrop(this.objectCtrl.GetObjectAttribute().dropList);
        this.OnDeadAddKill();
        AudioManager.Instance.SpawnSFX(SoundCode.KillEnemy);
    }
    protected virtual void OnDeadAddKill() 
    {
        MapStatistics mapStatistics = MapStatistics.Instance;
        if (mapStatistics == null) return;
        mapStatistics.AddKill();
    }
    protected virtual void OnDeadDrop(List<ItemDropRate> itemDropRates)
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = Quaternion.identity;
        ItemPickupSpawner.Instance.Drop(itemDropRates, dropPos, dropRot);
    }
    


}

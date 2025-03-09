using System.Collections;
using UnityEngine;

public abstract class BossAbility : FuncManager
{
    [SerializeField] protected float cooldownTime; 
    [SerializeField] protected  float lastUsedTime;
    [SerializeField] protected Transform PlayerPos;
    protected override void Start()
    {
        base.Start();
        this.LoadPlayerPos();
    }
    protected virtual void LoadPlayerPos()
    {
        if (this.PlayerPos != null) return;
        this.PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(transform.name + "Load PlayerPos ", gameObject);
    }
    public virtual bool CanUse()
    {
        return Time.time >= this.lastUsedTime + this.cooldownTime;
    }
    protected void UpdateLastUsedTime()
    {
        this.lastUsedTime = Time.time;
    }
    public abstract IEnumerator Execute(Transform shotPoint, BossStateMachine bossStateMachine, BossState bossState);
}

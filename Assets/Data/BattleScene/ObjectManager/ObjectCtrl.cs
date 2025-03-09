using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectCtrl : FuncManager
{
    [Header("Data")]
    [SerializeField] protected ObjectData objectSO;
    [SerializeField] protected ObjectAttribute objectAttribute;
    [Header("Body")]
    [SerializeField] protected Rigidbody2D ObjectRb2D;
    [SerializeField] protected ObjectModel objectModel; 
    [Header("Ability")]

    [SerializeField] protected ObjectMove objectMove;
    [SerializeField] protected Despawner Despawner;
    [SerializeField] protected ObjectDamageReceiver DamageReceiver;
    [SerializeField] protected ObjectDamageSender DamageSender;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadObjectSO();

        this.LoadRigibody();

        this.LoadObjectModel();
        this.LoadObjectMove();
        this.LoadDespawner();
        this.LoadObjectDamageReceiver();
        this.LoadDamageSender();
    }
    protected virtual void LoadObjectSO()
    {
        if (this.objectSO != null) return;
        string resPath = this.GetObjectTypeString() + "/" + transform.name;
        this.objectSO = Resources.Load<ObjectData>(resPath);
        Debug.LogWarning(transform.name + ": Load ObjectSO " + resPath);
    }

    protected virtual void LoadRigibody()
    {
        if (this.ObjectRb2D != null) return;
        this.ObjectRb2D = transform.GetComponent<Rigidbody2D>();
        this.ObjectRb2D.gravityScale = 0;
        Debug.Log(transform.name + "Load RigiBody ", gameObject);
    }

    protected virtual void LoadObjectMove()
    {
        if (this.objectMove != null) return;
        this.objectMove = transform?.GetComponentInChildren<ObjectMove>();
        Debug.Log(transform.name + "Load ObjectMove ", gameObject);
    }
    protected virtual void LoadObjectModel()
    {
        if (this.objectModel != null) return;
        this.objectModel = transform.GetComponentInChildren<ObjectModel>();
        Debug.Log(transform.name + "Load ObjectModel ", gameObject);
    }
    protected virtual void LoadDespawner()
    {
        if (this.Despawner != null) return;
        this.Despawner = transform.GetComponentInChildren<Despawner>();
        Debug.Log(transform.name + ": LoadDespawner", gameObject);
    }
    protected virtual void LoadObjectDamageReceiver()
    {
        if (this.DamageReceiver != null) return;
        this.DamageReceiver = transform.GetComponentInChildren<ObjectDamageReceiver>();
        Debug.Log(transform.name + ": LoadObjectDamageReceiver", gameObject);
    }
    protected virtual void LoadDamageSender()
    {
        if (this.DamageSender != null) return;
        this.DamageSender = transform.GetComponentInChildren<ObjectDamageSender>();
        Debug.Log(transform.name + ": LoadDamageSender", gameObject);
    }
    public Rigidbody2D GetRigidbody2D() => this.ObjectRb2D;
    public ObjectData GetObjectData() => this.objectSO;
    public ObjectAttribute GetObjectAttribute() => this.objectAttribute;
    public ObjectModel GetObjectModel() => this.objectModel;
    public ObjectMove GetObjectMove() => this.objectMove;
    public Despawner GetDespawner() => this.Despawner;   
    public ObjectDamageReceiver GetDamageReceiver() => this.DamageReceiver;
    public ObjectDamageSender GetDamageSender() => this.DamageSender;
    public virtual void SetObjectAttribute(EnemyType enemyType)
    {
       //for override 
    }

    protected abstract string GetObjectTypeString();
}

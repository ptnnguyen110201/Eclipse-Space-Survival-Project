using UnityEngine;

public class ObjectDamageSender : DamageSender
{
    [SerializeField] protected ObjectCtrl objectCtrl;
    protected override void OnEnable()
    {
        this.SetATK();
        base.OnEnable();
        
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadObjectCtrl();
    }
    protected virtual void SetATK()
    {
        ObjectAttribute objectAttribute = this.objectCtrl.GetObjectAttribute();
        if (objectAttribute == null)
        {
            Debug.Log("ObjectAttribute is Null");
            return;
        }
        this.ATK = objectAttribute.objectATK;
    }
    protected virtual void LoadObjectCtrl()
    {
        if (this.objectCtrl != null) return;
        this.objectCtrl = transform.parent.GetComponent<ObjectCtrl>();
        Debug.Log(transform.name + ": LoadObjectCtrl", gameObject);
    }

    public override void Send(DamageReceiver damageReceiver)
    {
        base.Send(damageReceiver);
    }


  

}

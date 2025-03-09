using UnityEngine;

public abstract class ObjectAbstract : FuncManager
{
    [SerializeField] protected ObjectCtrl objectCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadObjectCtrl();
    }
    protected virtual void LoadObjectCtrl()
    {
        if (this.objectCtrl != null) return;
        this.objectCtrl = transform.parent.GetComponent<ObjectCtrl>();
        Debug.Log(transform.name + ": LoadObjectCtrl", gameObject);
    }
}

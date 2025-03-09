using UnityEngine;

public abstract class ShipAbstract : FuncManager
{
    [SerializeField] protected ShipCtrl shipCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipCtrl();
    }
    protected virtual void LoadShipCtrl()
    {
        if (this.shipCtrl != null) return;
        this.shipCtrl = transform.parent.GetComponent<ShipCtrl>();
        Debug.Log(transform.name + ": LoadShipCtrl", gameObject);
    }
    public ShipCtrl GetShipCtrl() => this.shipCtrl;
}

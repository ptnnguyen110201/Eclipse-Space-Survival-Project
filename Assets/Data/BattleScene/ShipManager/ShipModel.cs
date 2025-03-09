using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel : ShipAbstract
{
    [SerializeField] protected SpriteRenderer Model;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.SetModel();
    }
    protected virtual void LoadModel() 
    {
        if (this.Model != null) return;
        this.Model = this.GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + "Load Model", gameObject);
    }

    public virtual void SetModel()
    {
        Sprite sprite = this.shipCtrl.GetShipData().sprite;
        if (sprite == null) return;
        this.Model.sprite = sprite;
    }
}

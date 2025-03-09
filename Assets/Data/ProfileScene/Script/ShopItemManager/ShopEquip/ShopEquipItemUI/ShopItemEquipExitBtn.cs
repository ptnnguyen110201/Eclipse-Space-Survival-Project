using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemEquipExitBtn : ButtonBase
{
    [SerializeField] protected Transform Stat;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStat();
    }

    protected virtual void LoadStat() 
    {
        if (this.Stat != null) return;
        this.Stat = transform.parent.parent.parent.GetComponent<Transform>();
        Debug.Log(transform.name + "Load Stat ", gameObject);
    }
    protected override void OnClick()
    {
        UIManager.Instance.CloseScaleUp(this.Stat.transform.gameObject, 0.25f);
    }
}

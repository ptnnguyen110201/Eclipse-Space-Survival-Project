using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopEquipItemRefreshTime : FuncManager
{
    [SerializeField] protected TextMeshProUGUI refeshPrice;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRefeshPrice();
    }
    public void SetUI(string Timer)
    {
        if (Timer == null) return;
        this.refeshPrice.text = Timer;
    }
    protected virtual void LoadRefeshPrice()
    {
        if (this.refeshPrice != null) return;
        this.refeshPrice = transform.Find("Value").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadRefeshPrice ", gameObject);
    }

}

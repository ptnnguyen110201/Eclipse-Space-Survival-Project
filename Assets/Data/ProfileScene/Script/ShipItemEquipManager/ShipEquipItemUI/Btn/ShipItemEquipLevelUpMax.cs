using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipItemEquipLevelUpMax : ButtonBase
{

    [SerializeField] protected ShipItemEquipData shipItemEquip;
    [SerializeField] protected ShipItemEquipStat shipItemEquipStat;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipItemEquipStat();
    }
    protected virtual void LoadShipItemEquipStat()
    {
        if (this.shipItemEquipStat != null) return;
        this.shipItemEquipStat = transform.parent.parent.GetComponent<ShipItemEquipStat>();
        Debug.Log(transform.name + "Load ShipItemEquipStat", gameObject);
    }

    public void ShowUI(ShipItemEquipData shipItemEquip)
    {
        if (shipItemEquip == null) return;
        this.shipItemEquip = shipItemEquip;
    }

    protected override void OnClick()
    {
        for (int i = 0; i < this.shipItemEquip.GetMaxLevel(); i++)
        {
            EquipmentBarManager.Instance.UpLevelItem(this.shipItemEquip, 1);

        }

    }
}

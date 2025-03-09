using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipItemEquipBtn : ButtonBase
{

    [SerializeField] protected ShipItemEquipData shipItemEquip;
    [SerializeField] protected ShipItemEquipStat shipItemEquipStat;
    [SerializeField] protected TextMeshProUGUI Item_EquipBtnText;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipItemEquipStat();
        this.LoadItem_EquipBtnText();
    }
    protected virtual void LoadShipItemEquipStat()
    {
        if (this.shipItemEquipStat != null) return;
        this.shipItemEquipStat = transform.parent.parent.GetComponent<ShipItemEquipStat>();
        Debug.Log(transform.name + "Load ShipItemEquipStat", gameObject);
    }
    protected virtual void LoadItem_EquipBtnText()
    {
        if (this.Item_EquipBtnText != null) return;
        this.Item_EquipBtnText = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + "Load Item_EquipBtnText", gameObject);
    }

    public void ShowUI(ShipItemEquipData shipItemEquip)
    {
        if (shipItemEquip == null) return;
        this.shipItemEquip = shipItemEquip;
        if (shipItemEquip.isEquipped)
        {
            this.Item_EquipBtnText.text = $"Unequip";
        }
        else
        {
            this.Item_EquipBtnText.text = $"Equip";
        }
    }

    protected override void OnClick()
    {
        if (this.shipItemEquip.isEquipped)
        {
            EquipmentBarManager.Instance.UnequipItem(this.shipItemEquip);
        }
        else
        {
            EquipmentBarManager.Instance.EquipItem(this.shipItemEquip);
            AudioManager.Instance.SpawnSFX(SoundCode.EquipSound);
        }
        EquipmentBarManager.Instance.CloseStat();



    }
}

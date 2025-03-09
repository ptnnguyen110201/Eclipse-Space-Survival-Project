using System.Collections;
using TMPro;
using UnityEngine;

public class ShipUIDetail : FuncManager
{
    [SerializeField] protected ShipItemEquipDatas ShipItemEquipDatas;
    [SerializeField] protected TextMeshProUGUI ShipHPValue;
    [SerializeField] protected TextMeshProUGUI ShipATKValue;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadShipHPValue();
        LoadShipATKValue();
    }

    protected virtual void LoadShipHPValue()
    {
        ShipHPValue = transform.Find("ShipHp").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load ShipHPValue", gameObject);
    }

    protected virtual void LoadShipATKValue()
    {
        ShipATKValue = transform.Find("ShipATK").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load ShipATKValue", gameObject);
    }

    public void SetShipDetailUI(ShipItemEquipDatas ShipItemEquipDatas)
    {
        if (ShipItemEquipDatas == null) return;

        this.StartCoroutine(UpdateShipStats(ShipItemEquipDatas));
    }

    private IEnumerator UpdateShipStats(ShipItemEquipDatas ShipItemEquipDatas)
    {
        float duration = 0.5f;
        int completedCoroutines = 0;

        UIManager.Instance.IncreaseValue(this.ShipItemEquipDatas.currentHP, ShipItemEquipDatas.currentHP, duration, this.ShipHPValue);
        UIManager.Instance.IncreaseValue(this.ShipItemEquipDatas.currentATK, ShipItemEquipDatas.currentATK, duration, this.ShipATKValue);
        this.ShipItemEquipDatas.currentHP = ShipItemEquipDatas.currentHP;
        this.ShipItemEquipDatas.currentATK = ShipItemEquipDatas.currentATK;

        yield return new WaitUntil(() => completedCoroutines >= 2);

    }
}

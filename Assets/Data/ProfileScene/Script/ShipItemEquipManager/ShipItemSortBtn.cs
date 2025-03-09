using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipItemSortBtn : ButtonBase
{
    [SerializeField] protected TextMeshProUGUI SortTextUIBtn;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSortTextUI();
    }

    protected virtual void LoadSortTextUI()
    {
        if (this.SortTextUIBtn != null) return;
        this.SortTextUIBtn = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + "LoadSortTextUI", gameObject);
    }
    protected override void OnClick()
    {
        SortType sortType = EquipmentBarManager.Instance.ChangeSortType();
        this.SortTextUIBtn.text = sortType.ToString();
    }
}

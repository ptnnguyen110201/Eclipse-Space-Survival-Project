using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICurrencySlot : ButtonBase
{
    [SerializeField] public TextMeshProUGUI currentCyText;
    [SerializeField] public int Value;
    [SerializeField] public CurrencyType currencyType;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCurrencyText();
    }

    protected virtual void LoadCurrencyText()
    {
        if (this.currentCyText != null) return;
        this.currentCyText = transform.Find("Value").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadCurrencyText", gameObject);
    }

    public void SetValue(int Value, int Max = 0)
    {
        if (Value < 0) return;
        this.Value = Value;
        if (Max > 0)
        {
            UIManager.Instance.IncreaseValue(this.Value, Value, Max, 0.5f, this.currentCyText);
            return;
        }

        UIManager.Instance.IncreaseValue(this.Value, Value, 0.5f, this.currentCyText);
    }

    protected override void OnClick()
    {
        if (this.currencyType == CurrencyType.Diamonds) return;
        CurrencyManager.Instance.OpenChangeCurrenCy(this.currencyType);
    }
}

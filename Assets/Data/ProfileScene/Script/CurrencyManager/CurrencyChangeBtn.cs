using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCurrencyBtn : ButtonBase
{
    [SerializeField] protected CurrencyType currencyType;
    public void ShowUI(PlayerData playerData, CurrencyType currencyType) 
    {
        if (playerData == null) return;
        this.currencyType = currencyType;
        bool isEnought = playerData.currencyData.IsEnoughtDiamonds();
        if (isEnought)
        {
            this.button.interactable = true;
            return;
        }
        this.button.interactable = false;
    }
    protected override void OnClick()
    {
        CurrencyManager.Instance.ChangeCurrenCy(currencyType);
    }
}

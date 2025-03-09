using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopEquipItemRefreshBtn : ButtonBase
{
    [SerializeField] protected TextMeshProUGUI refeshPrice;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRefeshPrice();
    }
    public void SetUI(PlayerData playerData) 
    {
        int currentPrice = playerData.currencyData.playerDiamonds.Amount;
        int refeshPrice = playerData.shopEquipItemDatas.refreshCost.Amount;
        this.refeshPrice.text = refeshPrice.ToString();
        if (currentPrice >= refeshPrice)
        {
            this.refeshPrice.color = Color.white;
            this.button.interactable = true;
        }
        else 
        {
            this.refeshPrice.color = Color.red;
            this.button.interactable = false;
        }
    }
    protected virtual void LoadRefeshPrice() 
    {
        if (this.refeshPrice != null) return;
        this.refeshPrice = transform.Find("Value").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadRefeshPrice ", gameObject);
    }
    protected override void OnClick()
    {
        ShopEquipItemManager.Instance.RefreshShop();
    }
}

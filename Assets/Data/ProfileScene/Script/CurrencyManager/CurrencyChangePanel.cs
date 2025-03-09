using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CurrencyChangePanel : FuncManager 
{
    [SerializeField] protected TextMeshProUGUI ChangeType;

    [SerializeField] protected Image ChangeValue1Icon;
    [SerializeField] protected Image ChangeValue2Icon;

    [SerializeField] protected TextMeshProUGUI ChangeValue1;
    [SerializeField] protected TextMeshProUGUI ChangeValue2;

    [SerializeField] protected ChangeCurrencyBtn changeCurrencyBtn;
    [SerializeField] protected ChangePanelExitBtn changePanelExitBtn;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadChangeType();
        this.LoadChangeValue1();
        this.LoadChangeValue2();
        this.LoadChangeValue1Icon();
        this.LoadChangeValue2Icon();
        this.LoadChangeCurrencyBtn();
        this.LoadChangePanelExitBtn();
    }

    public virtual void SetChangeUI(PlayerData playerData, CurrencyType currencyType) 
    {
        this.ChangeType.text = $"{CurrencyType.Diamonds} Change To {currencyType}";
        this.ChangeValue1.text = $"1";
        this.ChangeValue1Icon.sprite = playerData.currencyData.playerDiamonds.diamondsData.DiamondsSprite;

        switch (currencyType)
        {
            case CurrencyType.Golds:
                this.ChangeValue2.text = "200";
                this.ChangeValue2Icon.sprite = playerData.currencyData.playerGolds.goldsData.GoldsSprite;
                break;

            case CurrencyType.Energy:
                this.ChangeValue2.text = "1";
                this.ChangeValue2Icon.sprite = playerData.currencyData.playerEnergy.energyData.EnergySprite;
                break;

            default:
                Debug.LogWarning($"Unsupported target currency type: {currencyType}");
                break;
        }
        this.changeCurrencyBtn.ShowUI(playerData, currencyType);
    }
    protected virtual void LoadChangeType() 
    {
        if (this.ChangeType != null) return;
        this.ChangeType = transform.Find("ChangeType").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadChangeType", gameObject);
    }
    protected virtual void LoadChangeValue1()
    {
        if (this.ChangeValue1 != null) return;
        this.ChangeValue1 = transform.Find("ChangeVale/Value1/Value").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadChangeValue1Icon", gameObject);
    }
    protected virtual void LoadChangeValue2()
    {
        if (this.ChangeValue2 != null) return;
        this.ChangeValue2 = transform.Find("ChangeVale/Value2/Value").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadChangeValue1Icon", gameObject);
    }

    protected virtual void LoadChangeValue1Icon()
    {
        if (this.ChangeValue1Icon != null) return;
        this.ChangeValue1Icon = transform.Find("ChangeVale/Value1/Image").GetComponent<Image>();
        Debug.Log(transform.name + " LoadChangeValue1Icon", gameObject);
    }
    protected virtual void LoadChangeValue2Icon()
    {
        if (this.ChangeValue2Icon != null) return;
        this.ChangeValue2Icon = transform.Find("ChangeVale/Value2/Image").GetComponent<Image>();
        Debug.Log(transform.name + " LoadChangeValue2Icon", gameObject);
    }

    protected virtual void LoadChangeCurrencyBtn()
    {
        if (this.changeCurrencyBtn != null) return;
        this.changeCurrencyBtn = transform.Find("Btn").GetComponentInChildren<ChangeCurrencyBtn>();
        Debug.Log(transform.name + " LoadChangeCurrencyBtn", gameObject);
    }
    protected virtual void LoadChangePanelExitBtn()
    {
        if (this.changePanelExitBtn != null) return;
        this.changePanelExitBtn = transform.Find("Btn").GetComponentInChildren<ChangePanelExitBtn>();
        Debug.Log(transform.name + " LoadChangePanelExitBtn", gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : FuncManager
{
    private static CurrencyManager instance;
    public static CurrencyManager Instance => instance;

    [SerializeField] private List<UICurrencySlot> UICurrencySlots;
    [SerializeField] protected Transform currencyChangePanel;
    [SerializeField] protected NewBieReward newBieReward;

    protected override void Awake()
    {
        if (CurrencyManager.instance != null) Debug.LogError("Only 1 CurrencyManager allowed to exist");
        CurrencyManager.instance = this;
    }
    protected override void Start()
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        playerData.currencyData.OnCurrencyUpdated += () => this.SetUIValue(playerData);
        this.SetUIValue(playerData);
        this.StartCoroutine(this.CheckEnergyRecovery(playerData));
        this.NewBieReward(playerData);

    }
    protected void OnDestroy()
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        playerData.currencyData.OnCurrencyUpdated -= () => this.SetUIValue(playerData);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUICurrencySlot();
        this.LoadCurrencyChangePanel();
        this.LoadNewBieReward();
    }
    public void NewBieReward(PlayerData playerData) 
    {
        if (playerData.newbieRewardDatas.IsReward) return;
        UIManager.Instance.OpenScaleUp(this.newBieReward.gameObject, 0.5f);
        this.newBieReward.ShowUIImage(playerData);
    }
    public virtual void OpenChangeCurrenCy(CurrencyType currencyType )
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if( playerData == null ) return;
        CurrencyChangePanel currencyChangePanel = this.currencyChangePanel.GetComponentInChildren<CurrencyChangePanel>();
        currencyChangePanel.SetChangeUI(playerData, currencyType);
        UIManager.Instance.OpenScaleUp(this.currencyChangePanel.gameObject, 0.5f);
    }
    public virtual void CloseChangeCurrenCy()
    {
        UIManager.Instance.CloseScaleUp(this.currencyChangePanel.gameObject, 0.5f);
    }
    public virtual void ChangeCurrenCy(CurrencyType currencyType) 
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        playerData.currencyData.CanConvertCurrencyToEnergy(playerData, 1,currencyType);
    }
    protected virtual void LoadUICurrencySlot()
    {
        if (this.UICurrencySlots.Count > 0) return;
        foreach (Transform obj in this.transform.Find("CurrenCyHUD"))
        {
            UICurrencySlot uICurrencySlot = obj.GetComponent<UICurrencySlot>();
            this.UICurrencySlots.Add(uICurrencySlot);
        }
    }
    protected virtual void LoadCurrencyChangePanel()
    {
        if (this.currencyChangePanel != null ) return;
        this.currencyChangePanel = transform.Find("ChangePanel").GetComponent<Transform>();
        this.currencyChangePanel.gameObject.SetActive(false);
        Debug.Log(transform.name + " LoadCurrencyChangePanel", gameObject);
    }

    protected virtual void LoadNewBieReward()
    {
        if (this.newBieReward != null) return;
        this.newBieReward = transform.GetComponentInChildren<NewBieReward>(true);
        this.newBieReward.gameObject.SetActive(false);
        Debug.Log(transform.name + " LoadNewBieReward", gameObject);
    }
    private IEnumerator CheckEnergyRecovery(PlayerData playerData)
    {
        if (playerData == null) yield break;
        while (true)
        {
            if (playerData != null)
            {
                bool recovered = playerData.currencyData.playerEnergy.RecoverEnergyByTime();
                if (recovered)
                {
                    playerData.currencyData.AddCurrency(playerData, 1, CurrencyType.Energy);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void SetUIValue(PlayerData playerData)
    {
        if (playerData == null) return;

        foreach (UICurrencySlot slot in this.UICurrencySlots)
        {
            if (slot == null) continue;
            switch (slot.currencyType)
            {
                case CurrencyType.Golds:
                    slot.SetValue(playerData.currencyData.playerGolds.Amount);
                    break;
                case CurrencyType.Diamonds:
                    slot.SetValue(playerData.currencyData.playerDiamonds.Amount);
                    break;
                case CurrencyType.Energy:
                    slot.SetValue(playerData.currencyData.playerEnergy.Amount, playerData.currencyData.playerEnergy.MaxAmount);
                    break;
                default:
                    Debug.LogWarning("No matching currency for: " + slot.currencyType);
                    break;
            }
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class ReviveMenu : FuncManager
{
    [SerializeField] protected TextMeshProUGUI reviveCount;
    [SerializeField] protected TextMeshProUGUI reviveCoins;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadReviveCount();
        this.LoadReviveCoins();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.ShowUI();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    public void ShowUI()
    {
     
        int ReviveCount = MapStatistics.Instance.GetMapStatisticsData().ReviveCount;
        int ReviveMax = MapStatistics.Instance.GetMapStatisticsData().MaxRevives;
        int ReviveCoins = MapStatistics.Instance.GetMapStatisticsData().ReviveCoins.Amount;

        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        int PlayerDiamon = PlayerDataLoad.Instance.GetPlayerData().currencyData.playerDiamonds.Amount;
        this.reviveCount.text = $"{ReviveCount}/{ReviveMax} can revive!";
        this.reviveCoins.text = $"{PlayerDiamon}/{ReviveCoins} use gems to revive";

    }
    public void Respawn() 
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        PlayerDiamonds ReviveCoins  = MapStatistics.Instance.GetMapStatisticsData().ReviveCoins;
        PlayerDataLoad.Instance.GetPlayerData().currencyData.SpendCurrency(playerData, ReviveCoins.Amount, ReviveCoins.diamondsData.CurrencyType); 
        MapStatistics.Instance.RevivePlayer();
        ShipManager.Instance.Respawn();
        ItemPickupSpawner.Instance.ItemSpawn(ItemType.Boom, ShipManager.Instance.GetShipCtrl().transform.position, Quaternion.identity);


    }

    protected virtual void LoadReviveCount()
    {
        if (this.reviveCount != null) return;
        this.reviveCount = transform.Find("Panel/ReviveStatic/ReviveCount").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + "Load ReviveCount", gameObject);
    }
    protected virtual void LoadReviveCoins()
    {
        if (this.reviveCoins != null) return;
        this.reviveCoins = transform.Find("Panel/ReviveStatic/ReviveCoins").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load ReviveCount", gameObject);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class PlayerDataLoad : FuncManager
{
    private static PlayerDataLoad instance;
    public static PlayerDataLoad Instance => instance;

    [SerializeField] protected PlayerData playerData;

    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
       this.LoadPlayerData();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.DeleteData();
    }
    private void LoadPlayerData()
    {
        this.playerData = SaveSystem.LoadPlayerData() ?? CreateDefaultPlayerData();
        SaveSystem.SavePlayerData(playerData);
    }

    private PlayerData CreateDefaultPlayerData()
    {
        PlayerData defaultData = new PlayerData
        {
            shipItemEquipDatas = new ShipItemEquipDatas(),
            currencyData = new CurrencyData
            {
                playerDiamonds = new()
                {
                    Amount = 1000,
                    diamondsData = Resources.Load<DiamondsData>("CurrencyDatas/DiamondsData"),
                },
                playerGolds = new()
                {
                    Amount = 100000,
                    goldsData = Resources.Load<GoldsData>("CurrencyDatas/GoldsData"),
                },
                playerEnergy = new()
                {
                    Amount = 30,
                    energyData = Resources.Load<EnergyData>("CurrencyDatas/EnergyData"),
                }
            },
            newbieRewardDatas = new NewbieRewardDatas() 
            {
                newbieRewardData = Resources.Load<NewbieRewardData>("NewBie/NewbieRewardData"),
            },
            shopEquipItemDatas = new ShopEquipItemDatas(),
            mapProgress = new MapProgress(),
            gameState = new GameState(),
            questDatas = new QuestData(),
            dailyCheckInData = new DailyCheckInData(),
        };
        defaultData.shopEquipItemDatas.UpdateShopItemsByTime();
        defaultData.mapProgress.LoadMapData();
        defaultData.questDatas.CheckAndUpdateDailyQuests();
        defaultData.dailyCheckInData.LoadAndGenerateRewards();
        return defaultData;
    }

    public PlayerData GetPlayerData() => playerData;

    private void DeleteData()
    {
        SaveSystem.DeletePlayerData();
    }

    public void SaveData()
    {
        SaveSystem.SavePlayerData(playerData);
    }

  
    
}

using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopEquipItemDatas
{    
    public List<ShopEquipItemData> currentShopItems;
    public ShopRefreshCurrency refreshCost;
    public int maxSpawnCount = 6;
    public string lastUpdateTime;
    public string nextUpdateTime;
    public int shopRefreshInterval = 1;

    public ShopEquipItemData GetShopItem(ShopEquipItemData shopEquiItemData)
    {
        foreach (ShopEquipItemData item in this.currentShopItems)
        {
            if (item == shopEquiItemData)
            {
                return item;
            }
        }
        return null;
    }
   
    public bool UpdateShopItemsByTime()
    {
        if (!this.IsTimeToUpdateShop()) return false;
        this.SetRefreshShopCost();
        this.currentShopItems = this.RandomizeShopItems(this.maxSpawnCount);
        this.lastUpdateTime = DateTime.Now.ToString();
        this.nextUpdateTime = this.CalculateNextUpdateTime(this.lastUpdateTime);
        return true;
    }
    public void UpdateShopItemByDiamonds(PlayerData playerData)
    {
        bool CanRefesh = playerData.currencyData.SpendCurrency(playerData,this.refreshCost.Amount, this.refreshCost.diamondsData.CurrencyType);
        if (!CanRefesh)
        {
            Debug.Log("Currency isn't enought to refesh");
            return;
        }
        this.currentShopItems = RandomizeShopItems(this.maxSpawnCount);
        SaveSystem.SavePlayerData(playerData);
    }
    public string GetRemainingTimeForShopUpdate()
    {
        if (string.IsNullOrEmpty(this.nextUpdateTime))
        {
            return "Shop is available for update now.";
        }

        DateTime nextUpdate;
        if (!DateTime.TryParse(this.nextUpdateTime, out nextUpdate))
        {
            return "Invalid next update time.";
        }

        TimeSpan remainingTime = nextUpdate - DateTime.Now;

        if (remainingTime.TotalSeconds > 0)
        {
            return $"{remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
        }
        return $"00:00:00";
    }


    private void SetRefreshShopCost() 
    {
        DiamondsData diamondsData = Resources.Load<DiamondsData>("CurrencyDatas/DiamondsData");
        this.refreshCost = new()
        {
            Amount = 50,
            diamondsData = diamondsData
        };
    }
    private List<ShopEquipItemData> RandomizeShopItems(int numberOfItems)
    {
        ShipItemData[] allItems = Resources.LoadAll<ShipItemData>("ShipItemDatas");

        List<ShopEquipItemData> selectedItems = new List<ShopEquipItemData>();
        List<ShipItemData> filteredItems = new List<ShipItemData>();
        foreach (var item in allItems)
        {
            if (item.ItemID == "1")
            {
                filteredItems.Add(item);
            }
        }

        for (int i = 0; i < numberOfItems; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, filteredItems.Count);
            ShipItemData randomItem = filteredItems[randomIndex];

            ShopEquipItemData shopItemData = new ShopEquipItemData();
            shopItemData.SetState(randomItem, false);

            int randomTier = UnityEngine.Random.Range(1, 2);
            shopItemData.currentTier = randomTier;
            shopItemData.currentLevel = 1;
            selectedItems.Add(shopItemData);
        }

        return selectedItems;
    }


    private bool IsTimeToUpdateShop()
    {
        if (string.IsNullOrEmpty(this.lastUpdateTime))
            return true;

        DateTime lastUpdate;
        if (!DateTime.TryParse(this.lastUpdateTime, out lastUpdate))
            return true;

        TimeSpan timeSinceLastUpdate = DateTime.Now - lastUpdate;
        return timeSinceLastUpdate.TotalHours >= this.shopRefreshInterval;
    }
    private string CalculateNextUpdateTime(string lastUpdateTime)
    {
        DateTime lastUpdate;
        if (DateTime.TryParse(lastUpdateTime, out lastUpdate))
        {
            DateTime nextUpdate = lastUpdate.AddHours(this.shopRefreshInterval);
            return nextUpdate.ToString();
        }
        return DateTime.Now.AddHours(this.shopRefreshInterval).ToString();
    }
}

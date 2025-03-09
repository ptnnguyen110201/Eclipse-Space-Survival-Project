using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MapProgress
{
    public List<MapResult> mapResults;
    public MapCost mapCost;
    public void SetMapCost()
    {
        this.mapCost = new MapCost()
        {
            Amount = 5,
            energyData = Resources.Load<EnergyData>("CurrencyDatas/EnergyData"),
        };
    }

    public MapResult GetMapResult(int mapID)
    {
        var result = mapResults.Find(m => m.mapdata.MapID == mapID);

        if (result == null)
        {
            Debug.LogWarning($"No MapResult found for MapID: {mapID}");
            return null;
        }

        return result;
    }
    public bool DeductEnergyToPlayMap(PlayerData playerData)
    {
        return playerData.currencyData.SpendCurrency(playerData, this.mapCost.Amount, mapCost.energyData.CurrencyType);
    }

    public void LoadMapData()
    {
        this.SetMapCost();
        Mapdata[] allMaps = Resources.LoadAll<Mapdata>("MapDatas");
        if (allMaps == null || allMaps.Length == 0)
        {
            Debug.LogWarning("No Mapdata found in Resources/MapDatas");
            return;
        }

        if (this.mapResults == null)
            this.mapResults = new List<MapResult>();
        else
            this.mapResults.Clear();

        foreach (Mapdata map in allMaps)
        {
            MapResult mapResult = new MapResult
            {
                mapdata = map,
                IsCompleted = false,
                IsRewardClaimed = false,
                IsUnlocked = map.MapID == 1, 
            };
            this.mapResults.Add(mapResult);
        }
        this.mapResults.Sort((a, b) => a.mapdata.MapID.CompareTo(b.mapdata.MapID));
    }
}

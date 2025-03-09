using UnityEngine;

[System.Serializable]
public class MapResult
{
    public Mapdata mapdata;
    public bool IsUnlocked;
    public bool IsCompleted;
    public bool IsRewardClaimed;

    public void SetMapCompleted()
    {
        if (mapdata == null)
        {
            Debug.LogWarning("Mapdata is null. Cannot complete map.");
            return;
        }
        IsCompleted = true;
    }

    public void SetMapUnlocked()
    {
        if (mapdata == null)
        {
            Debug.LogWarning("Mapdata is null. Cannot unlock map.");
            return;
        }
        IsUnlocked = true;
    }

    public void SetRewardClaimed()
    {
        if (mapdata == null)
        {
            Debug.LogWarning("Mapdata is null. Cannot claim reward.");
            return;
        }
        IsRewardClaimed = true;
    }
    public void ClaimReward(PlayerData playerData)
    {
        if (!IsCompleted)
        {
            Debug.LogWarning($"Map {mapdata.MapID} has not been completed. Cannot claim reward.");
            return;
        }

        if (IsRewardClaimed)
        {
            Debug.LogWarning($"Reward for map {mapdata.MapID} has already been claimed.");
            return;
        }

        if (mapdata.mapReward == null || mapdata.mapReward == null)
        {
            Debug.LogWarning($"Map {mapdata.MapID} does not have a valid reward.");
            return;
        }
        MapReward reward = mapdata.mapReward;
        if (reward == null) return;
        playerData.currencyData.AddCurrency(playerData, reward.Amount, reward.diamondsData.CurrencyType);

        this.IsRewardClaimed = true;
        PlayerDataLoad.Instance.SaveData();

    }
}

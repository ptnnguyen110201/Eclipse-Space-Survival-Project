using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapStatisticsData
{
    public int TotalKills;
    public int TotalEarnings;
    public int TotalTimer;

    public PlayerDiamonds ReviveCoins;
    public int ReviveCount;
    public int MaxRevives = 2; 
    public void UpdateQuestProgress(PlayerData playerData) 
    {
        playerData.questDatas.UpdateQuestProgress(QuestType.KillEnemies, TotalKills);
        int totalTimer = (int)(TotalTimer / 60);
        playerData.questDatas.UpdateQuestProgress(QuestType.PlayTime, totalTimer);
    }
    public bool CanRevive() => this.ReviveCount > 0;
    public void Revive() =>this.ReviveCount--;
    public void AddKill() => this.TotalKills ++;
    public void AddEarnings(int amount) => this.TotalEarnings += Mathf.Max(0, amount);
    public void IncrementTimer(int amount) => this.TotalTimer += amount;
    public void Reset()
    {
        TotalKills = 0;
        TotalEarnings = 0;
        TotalTimer = 0;
        ReviveCount = MaxRevives;
        this.ReviveCoins = new PlayerDiamonds()
        {
            Amount = 20,
            diamondsData = Resources.Load<DiamondsData>("CurrencyDatas/DiamondsData"),
        };
    }
}

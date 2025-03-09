using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class ShipLevel : ShipAbstract
{
    [Header("Level Ship")]
    [SerializeField] private double currentEXP;
    [SerializeField] private double requiredExp;
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxLevel;

    public double GetCurrentExp() => this.currentEXP;
    public double GetRequiredExp() => this.requiredExp;
    public int GetCurrentLevel() => this.currentLevel;

    protected override void Start()
    {
        base.Start();
        this.InitializeLevelData();
    }

    private void InitializeLevelData()
    {
        this.currentLevel = 1;
        this.maxLevel = this.shipCtrl.GetShipData().shipLevelMap.levels.Count;
        this.UpdateRequiredExp();
    }

    public void AddEXP(int amount)
    {
        this.currentEXP += amount;
        GameEvents.TriggerEvent(GameEventType.AddExp);
        Debug.Log(GameEvents.eventMap.Count);
        this.HandleLevelUp();
    }

    private void HandleLevelUp()
    {
        int newLevel = this.shipCtrl.GetShipData().shipLevelMap.GetLevel(this.currentEXP);

        while (newLevel > this.currentLevel && this.currentLevel < this.maxLevel)
        {
            this.ProcessLevelUp();
            newLevel = this.shipCtrl.GetShipData().shipLevelMap.GetLevel(this.currentEXP);
        }

        if (this.currentLevel >= this.maxLevel)
        {
            this.currentEXP = 0;
        }
    }

    private void ProcessLevelUp()
    {
        double expForCurrentLevel = this.shipCtrl.GetShipData().shipLevelMap.ExpNextLevel(this.currentLevel);

        if (this.currentLevel < this.maxLevel)
        {
            this.DeductExp(expForCurrentLevel);
            this.IncreaseLevel();
            GameEvents.TriggerEvent(GameEventType.LevelUp);
        }
    }

    private void DeductExp(double amount)
    {
        if (this.currentLevel >= this.maxLevel)
        {
            this.currentEXP = 0;
        }
        else
        {
            this.currentEXP -= amount;
        }
    }

    private void IncreaseLevel()
    {
        this.currentLevel++;
        this.UpdateRequiredExp();
    }

    private void UpdateRequiredExp()
    {
        if (this.currentLevel < this.maxLevel)
        {
            this.requiredExp = this.shipCtrl.GetShipData().shipLevelMap.ExpNextLevel(this.currentLevel);
        }
        else
        {
            this.requiredExp = 0;
        }
    }

    
}

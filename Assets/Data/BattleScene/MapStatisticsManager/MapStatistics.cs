using System;
using System.Collections;
using UnityEngine;

public class MapStatistics : FuncManager
{
    private static MapStatistics instance;
    public static MapStatistics Instance => instance;

    public event Action<int> OnKillsUpdated;
    public event Action<int> OnEarningsUpdated;
    public event Action<int> OnTimerUpdated;
    [SerializeField] protected Coroutine TimerCourotine;
    [SerializeField] protected MapStatisticsData statisticsData = new MapStatisticsData();

    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only one MapStatistics instance is allowed.");
            return;
        }
        instance = this;
       
    }
    protected override void Start()
    {
        base.Start();
        this.GetMapStatisticsData().Reset();
    }
    public void RevivePlayer()
    {
        if (this.statisticsData.CanRevive())
        {
       
            this.statisticsData.Revive();  
          
        }
   
    }
    public void AddKill()
    {
        this.statisticsData.AddKill();
        this.OnKillsUpdated?.Invoke(statisticsData.TotalKills);
    }

    public void AddEarnings(int amount)
    {
        this.statisticsData.AddEarnings(amount);
        this.OnEarningsUpdated?.Invoke(statisticsData.TotalEarnings);
    }
    public void GameTimerStart(int waveDuration) 
    {
        if (this.TimerCourotine != null)
        {
            this.StopCoroutine(this.TimerCourotine);
        }
        this.TimerCourotine = StartCoroutine(GameTimer(waveDuration));
    }
    private IEnumerator GameTimer(int waveDuration)
    {
        for (int i = 0; i < waveDuration; i++)
        {
            this.statisticsData.IncrementTimer(1);
            this.OnTimerUpdated?.Invoke(statisticsData.TotalTimer);
            yield return new WaitForSeconds(1f);
        }
    }


    public MapStatisticsData GetMapStatisticsData() => this.statisticsData;
 
}

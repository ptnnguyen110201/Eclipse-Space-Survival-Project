using TMPro;
using UnityEngine;

public class MapStatisticsUI : FuncManager
{
    [SerializeField] protected MapResourceUI mapResourceUI;
    [SerializeField] protected MapTimerUI mapTimerUI;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapResourceUI();
        this.LoadMapTimerUI();
    }
    protected virtual void LoadMapResourceUI()
    {
        if (this.mapResourceUI != null) return;
        this.mapResourceUI = transform.GetComponentInChildren<MapResourceUI>();
        Debug.Log(transform.name + "Load MapResourceUI ", gameObject);
    }
    protected virtual void LoadMapTimerUI()
    {
        if (this.mapTimerUI != null) return;
        this.mapTimerUI = transform.GetComponentInChildren<MapTimerUI>();
        Debug.Log(transform.name + "Load MapTimerUI ", gameObject);
    }
    protected override void OnEnable()
    {
        MapStatistics.Instance.OnKillsUpdated += UpdateKillsUI;
        MapStatistics.Instance.OnEarningsUpdated += UpdateEarningsUI;
        MapStatistics.Instance.OnTimerUpdated += UpdateTimerUI; 
    }

    protected override void OnDisable()
    {
        MapStatistics.Instance.OnKillsUpdated -= UpdateKillsUI;
        MapStatistics.Instance.OnEarningsUpdated -= UpdateEarningsUI;
        MapStatistics.Instance.OnTimerUpdated -= UpdateTimerUI; 
    }
    private void UpdateKillsUI(int totalKills)
    {
        this.mapResourceUI.UpdateKillsUI(totalKills);
    }

    private void UpdateEarningsUI(int totalEarnings)
    {
        this.mapResourceUI.UpdateEarningsUI(totalEarnings);
    }
    private void UpdateTimerUI(int totalTimer)
    {
        this.mapTimerUI.SetTimer(totalTimer);
    }
}

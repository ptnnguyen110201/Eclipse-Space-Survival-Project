using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : FuncManager
{
    private static BattleManager instance;
    public static BattleManager Instance => instance;

    [SerializeField] protected MapManager mapManager;
    [SerializeField] protected QuestManager questManager;
    [SerializeField] protected DailyCheckinManager dailyCheckinManager;
    [SerializeField] protected SettingsManager settingsManager;
    [SerializeField] protected OnWInStatManager onWInStatManager;
    protected override void Awake()
    {
        if (BattleManager.instance != null) Debug.LogError("Only 1 BattleManager allowed to exist");
        BattleManager.instance = this;

    }
    protected override void Start()
    {
        base.Start();
        this.OpenBattle();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        UIManager.Instance.CloseElement(this.mapManager.gameObject);
        UIManager.Instance.CloseElement(this.questManager.gameObject);
        UIManager.Instance.CloseElement(this.dailyCheckinManager.gameObject);
        UIManager.Instance.CloseElement(this.settingsManager.gameObject);
        UIManager.Instance.CloseElement(this.onWInStatManager.gameObject);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMapManager();
        this.LoadQuestManager();
        this.LoadDailyCheckingManager();
        this.LoadSettingPanel();
        this.LoadOnWInStatManager();
    }

    public void OpenBattle()
    {
        UIManager.Instance.OpenElement(this.mapManager.gameObject);
        UIManager.Instance.OpenElement(this.transform.gameObject);
        UIManager.Instance.CloseUIElementsExcept(this.transform.gameObject);

    }
    protected virtual void LoadMapManager()
    {
        if (this.mapManager != null) return;
        this.mapManager = transform.GetComponentInChildren<MapManager>();
        Debug.Log(transform.name + "Load MapManager ", gameObject);
    }
    protected virtual void LoadQuestManager()
    {
        if (this.questManager != null) return;
        this.questManager = transform.GetComponentInChildren<QuestManager>();
        Debug.Log(transform.name + "Load QuestManager ", gameObject);
    }
    protected virtual void LoadDailyCheckingManager()
    {
        if (this.dailyCheckinManager != null) return;
        this.dailyCheckinManager = transform.GetComponentInChildren<DailyCheckinManager>();
        Debug.Log(transform.name + "Load DailyCheckingManager ", gameObject);
    }

    protected virtual void LoadSettingPanel()
    {
        if (this.settingsManager != null) return;
        this.settingsManager = transform.GetComponentInChildren<SettingsManager>();
        Debug.Log(transform.name + "Load SettingPanel ", gameObject);
    }
    protected virtual void LoadOnWInStatManager()
    {
        if (this.onWInStatManager != null) return;
        this.onWInStatManager = transform.GetComponentInChildren<OnWInStatManager>();
        Debug.Log(transform.name + "Load OnWInStatManager", gameObject);
    }
    public MapManager GetMapManager() => this.mapManager;
    public QuestManager GetQuestManager() => this.questManager;
    public DailyCheckinManager GetDailyCheckingManager() => this.dailyCheckinManager;
    public SettingsManager GetSettingPanel() => this.settingsManager;
    public OnWInStatManager GetWInStatManager() => this.onWInStatManager;
}

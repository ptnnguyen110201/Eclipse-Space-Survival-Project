using System.Collections;
using UnityEngine;

public class ExpBarManager : FuncManager
{
    private static ExpBarManager instance;
    public static ExpBarManager Instance => instance;

    [SerializeField] protected ShipCtrl shipCtrl;
    [SerializeField] protected ExpSlider expSlider;
    [SerializeField] protected LevelText levelText;

    protected override void Awake()
    {
        if (ExpBarManager.instance != null) Debug.LogError("Only 1 ExpBarManager allowed to exist");
        ExpBarManager.instance = this;
    }

    public virtual void SetShipCtrl(ShipCtrl shipCtrl)
    {
        this.shipCtrl = shipCtrl;
        this.UpdateExpAndLevelUI();
        GameEvents.Subscribe(GameEventType.AddExp, this.UpdateExpAndLevelUI);
        GameEvents.Subscribe(GameEventType.LevelUp, this.UpdateExpAndLevelUI);
    }

  
    private void OnDestroy()
    {
        GameEvents.Unsubscribe(GameEventType.AddExp, this.UpdateExpAndLevelUI);
        GameEvents.Unsubscribe(GameEventType.LevelUp, this.UpdateExpAndLevelUI);
    }

    private void UpdateExpAndLevelUI()
    { 
        this.expSlider.SetCurrentValue(this.shipCtrl.GetShipLevel().GetCurrentExp());
        this.expSlider.SetMaxValue(this.shipCtrl.GetShipLevel().GetRequiredExp());
        this.levelText.SetLevelText(this.shipCtrl.GetShipLevel().GetCurrentLevel());

    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadExpSlider();
        this.LoadTextLevel();
    }

    protected virtual void LoadExpSlider()
    {
        if (this.expSlider != null) return;
        this.expSlider = transform.GetComponentInChildren<ExpSlider>();
        Debug.Log(transform.name + ": Load ExpSlider", gameObject);
    }

    protected virtual void LoadTextLevel()
    {
        if (this.levelText != null) return;
        this.levelText = transform.GetComponentInChildren<LevelText>();
        Debug.Log(transform.name + ": Load TextLevel", gameObject);
    }
}

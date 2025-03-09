
using System.Collections;
using TMPro;
using UnityEngine;

public class RewardBtn : ButtonBase
{
    [SerializeField] protected TextMeshProUGUI TotalGolds;
    [SerializeField] protected int TotalGoldCount;
    [SerializeField] protected bool CheckWin;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTotalGolds();
    }

    protected virtual void LoadTotalGolds()
    {
        if (this.TotalGolds != null) return;
        this.TotalGolds = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + "LoadTotalGolds", gameObject);
    }

    public void AnimateGoldCount(int TotalGoldCount, bool checkWin)
    {
        this.CheckWin = checkWin;
        this.TotalGoldCount = TotalGoldCount;
        float duration = 1f;
        UIManager.Instance.IncreaseValue(0, TotalGoldCount, duration, this.TotalGolds);
    }


    protected override void OnClick()
    {
        BattleManager.Instance.GetMapManager().RewardMapEnd(this.TotalGoldCount);  
        BattleManager.Instance.GetMapManager().Complete(CheckWin);
        BattleManager.Instance.GetMapManager().UnlockMap(CheckWin);
        BattleManager.Instance.GetMapManager().SpawnMap();
        BattleManager.Instance.GetWInStatManager().Close();
        PlayerDataLoad.Instance.SaveData();
    }
}


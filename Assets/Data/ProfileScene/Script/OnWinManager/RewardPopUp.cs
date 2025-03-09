using System.Collections;
using UnityEngine;
using TMPro;


public class RewardPopUp : FuncManager
{
    [SerializeField] protected TextMeshProUGUI Map_Name;
    [SerializeField] protected TextMeshProUGUI Map_TotalEnemy;
    [SerializeField] protected TextMeshProUGUI Map_TotalGolds;
    [SerializeField] protected Transform Map_Reward;
    [SerializeField] protected RewardBtn Map_RewardBtn;

    public virtual void ShowUI(PlayerData playerData, bool CheckWin)
    {
        this.ResetUI();

        if (playerData.gameState == null || playerData.gameState.SelectedMapData?.mapdata == null) return;
        this.Map_Name.transform.parent.gameObject.SetActive(true);
        this.Map_Name.text = playerData.gameState.SelectedMapData.mapdata.MapName;
        this.StartCoroutine(this.ShowRewardSequence(playerData, CheckWin));
    }

    private IEnumerator ShowRewardSequence(PlayerData playerData, bool CheckWin)
    {
        if (CheckWin) this.Map_Name.text = $"{playerData.gameState.SelectedMapData.mapdata.MapName} (Win)";
        if (!CheckWin) this.Map_Name.text = $"{playerData.gameState.SelectedMapData.mapdata.MapName} (Lost)";



        this.AnimateText(Map_TotalEnemy, playerData.gameState.MapStatistics.TotalKills, 1f);
        yield return new WaitForSeconds(0.5f);
        this.AnimateText(Map_TotalGolds, playerData.gameState.MapStatistics.TotalEarnings, 1f);
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.OpenScaleUp(this.Map_Reward.transform.gameObject, 0.25f);
        yield return new WaitForSeconds(0.5f);

        int totalGolds = playerData.gameState.MapStatistics.TotalEarnings + playerData.gameState.MapStatistics.TotalKills;
        if (!playerData.gameState.SelectedMapData.IsCompleted)
        {
            totalGolds *= 2;
        }
        UIManager.Instance.OpenScaleUp(this.Map_RewardBtn.transform.parent.gameObject, 0.25f);
        this.Map_RewardBtn.AnimateGoldCount(totalGolds, CheckWin);
    }

    private void AnimateText(TextMeshProUGUI textComponent, int targetValue, float duration)
    {
        UIManager.Instance.OpenScaleUp(textComponent.transform.parent.gameObject, 0.25f);
        int startValue = 0;
        int endValue = targetValue;
        UIManager.Instance.IncreaseValue(startValue, endValue, duration, textComponent);
    }

    protected void ResetUI()
    {
        this.Map_Name.text = string.Empty;
        this.Map_Name.transform.parent.gameObject.SetActive(false);

        this.Map_TotalEnemy.text = string.Empty;
        this.Map_TotalEnemy.transform.parent.gameObject.SetActive(false);

        this.Map_Reward.transform.gameObject.SetActive(false);

        this.Map_TotalGolds.text = string.Empty;
        this.Map_TotalGolds.transform.parent.gameObject.SetActive(false);

        this.Map_RewardBtn.transform.parent.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMap_Name();
        this.LoadMap_TotalEnemy();
        this.LoadMap_TotalGolds();
        this.LoadMap_Reward();
        this.LoadMap_RewardBtn();
    }

    // Load the components for Map_Name
    protected virtual void LoadMap_Name()
    {
        if (this.Map_Name != null) return;
        this.Map_Name = transform.Find("Map_Name").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadMap_Name", gameObject);
    }

    // Load the components for Map_TotalEnemy
    protected virtual void LoadMap_TotalEnemy()
    {
        if (this.Map_TotalEnemy != null) return;
        this.Map_TotalEnemy = transform.Find("Map_TotalEnemy").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadMap_TotalEnemy", gameObject);
    }

    // Load the components for Map_TotalGolds
    protected virtual void LoadMap_TotalGolds()
    {
        if (this.Map_TotalGolds != null) return;
        this.Map_TotalGolds = transform.Find("Map_TotalGolds").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadMap_TotalGolds", gameObject);
    }

    // Load the components for Map_Reward
    protected virtual void LoadMap_Reward()
    {
        if (this.Map_Reward != null) return;
        this.Map_Reward = transform.Find("Map_Reward").GetComponent<Transform>();
        Debug.Log(transform.name + " LoadMap_Reward", gameObject);
    }

    // Load the components for Map_RewardBtn
    protected virtual void LoadMap_RewardBtn()
    {
        if (this.Map_RewardBtn != null) return;
        this.Map_RewardBtn = transform.Find("Map_RewardBtn").GetComponentInChildren<RewardBtn>();
        Debug.Log(transform.name + " LoadMap_Reward", gameObject);
    }
}


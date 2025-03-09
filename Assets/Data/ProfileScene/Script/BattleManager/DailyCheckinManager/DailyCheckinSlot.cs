using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DailyCheckinSlot : ButtonBase
{

    [SerializeField] protected Transform Claim;
    [SerializeField] protected Transform UnClaimed;
    [SerializeField] protected Transform Claimed;
    [SerializeField] protected TextMeshProUGUI rewardDay;
    [SerializeField] protected Image rewardIcon;
    [SerializeField] protected TextMeshProUGUI rewardValue;
    [SerializeField] protected DailyRewardInstance dailyRewardInstance;
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadRewardDay();
        this.LoadRewardValue();
        this.LoadRewardIcon();

        this.LoadClaimed();
        this.LoadUnClaimed();
        this.LoadClaim();
    }

    public void ShowCheckinSlot(DailyRewardInstance dailyRewardInstance)
    {
        if (dailyRewardInstance == null) return;

        this.dailyRewardInstance = dailyRewardInstance;

        this.rewardDay.text = $"Day {dailyRewardInstance.Day}";
        this.rewardIcon.sprite = dailyRewardInstance.dailyReward.rewardSprite;
        this.rewardValue.text = $"{dailyRewardInstance.Amount}";

        DateTime today = DateTime.Now;
        bool canClaim = dailyRewardInstance.CanClaim(today);

        this.Claim.gameObject.SetActive(canClaim);
        this.UnClaimed.gameObject.SetActive(!canClaim && !dailyRewardInstance.isClaimedToday);
        this.Claimed.gameObject.SetActive(dailyRewardInstance.isClaimedToday);
    }

    protected virtual void LoadRewardDay()
    {
        if (this.rewardDay != null) return;
        this.rewardDay = transform.Find("RewardDay").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadRewardDay ", gameObject);
    }

    protected virtual void LoadRewardValue()
    {
        if (this.rewardValue != null) return;
        this.rewardValue = transform.Find("Claim/Value").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadRewardValue ", gameObject);
    }

    protected virtual void LoadRewardIcon()
    {
        if (this.rewardIcon != null) return;
        this.rewardIcon = transform.Find("Claim/Icon").GetComponent<Image>();
        Debug.Log(transform.name + " LoadRewardIcon ", gameObject);
    }

    protected virtual void LoadClaim()
    {
        if (this.Claim != null) return;
        this.Claim = transform.Find("Claim").GetComponent<Transform>();
        Debug.Log(transform.name + " LoadClaim ", gameObject);
    }
    protected virtual void LoadUnClaimed()
    {
        if (this.UnClaimed != null) return;
        this.UnClaimed = transform.Find("UnClaimed").GetComponent<Transform>();
        Debug.Log(transform.name + " LoadUnClaimed ", gameObject);
    }
    protected virtual void LoadClaimed()
    {
        if (this.Claimed != null) return;
        this.Claimed = transform.Find("Claimed").GetComponent<Transform>();
        Debug.Log(transform.name + " LoadClaimed ", gameObject);
    }
    protected override void OnClick()
    {
        if (!dailyRewardInstance.isClaimedToday)
        {
            DateTime today = DateTime.Now;
            PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
            dailyRewardInstance.ClaimReward(playerData, today);
        }
        this.ShowCheckinSlot(dailyRewardInstance);
    }
}
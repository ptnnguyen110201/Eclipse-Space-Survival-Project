using System;

[Serializable]
public class QuestInstance
{
    public Quest baseQuest; 
    public int currentAmount;
    public bool isCompleted;
    public bool isRewardClaimed;
    public QuestInstance(Quest quest)
    {
        baseQuest = quest;
        currentAmount = 0;
        isCompleted = false;
        isRewardClaimed = false;
    }

    public void UpdateProgress(int amount)
    {
        if (!isCompleted)
        {
            currentAmount += amount;
            if (currentAmount >= baseQuest.targetAmount)
            {
                currentAmount = baseQuest.targetAmount; 
                isCompleted = true;
            }
        }
    }

    public bool CanClaimReward()
    {
        return isCompleted && !isRewardClaimed;
    }
    public PlayerGolds ClaimReward()
    {
        if (CanClaimReward())
        {
            isRewardClaimed = true; 
            return baseQuest.questReward; 
        }
        return null;
    }
    public void AddReward(PlayerData playerData)
    {
        if (!CanClaimReward()) return;

        PlayerGolds reward = ClaimReward();
        if (reward != null && playerData != null)
        {
            playerData.currencyData.AddCurrency(PlayerDataLoad.Instance.GetPlayerData(), reward.Amount, CurrencyType.Golds);
         
        }
        PlayerDataLoad.Instance.SaveData();
    }
}

using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class DailyCheckInData
{
    public List<DailyRewardInstance> dailyRewards = new List<DailyRewardInstance>();

    public void LoadAndGenerateRewards()
    {
        DailyReward[] rewardTemplates = Resources.LoadAll<DailyReward>("DailyCheckin");
        this.GenerateDailyRewards(new List<DailyReward>(rewardTemplates));
    }

    public void GenerateDailyRewards(List<DailyReward> rewardTemplates)
    {
        DateTime now = DateTime.Now;
        int daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
        int goldBaseAmount = 2000;
        int diamondBaseAmount = 5;
        int itemBaseAmount = 1;
        int goldCount = 0, diamondCount = 0;

        for (int i = 1; i < daysInMonth + 1; i++)
        {
            DailyRewardInstance rewardInstance = new DailyRewardInstance();
            DailyReward rewardTemplate = null;

            if (i % 5 < 2)
            {
                rewardTemplate = rewardTemplates.Find(r => r.dailyRewardType == DailyRewardType.Golds);
                rewardInstance.Amount = goldBaseAmount;
                goldCount++;
                if (goldCount == 2)
                {
                    goldBaseAmount += 2000;
                    goldCount = 0;
                }
            }
            else if (i % 5 < 4)
            {
                rewardTemplate = rewardTemplates.Find(r => r.dailyRewardType == DailyRewardType.Diamonds);
                rewardInstance.Amount = diamondBaseAmount;
                diamondCount++;
                if (diamondCount == 2)
                {
                    diamondBaseAmount += 5;
                    diamondCount = 0;
                }
            }
            else
            {
                rewardTemplate = rewardTemplates.Find(r => r.dailyRewardType == DailyRewardType.Item);
                rewardInstance.Amount = itemBaseAmount;
                itemBaseAmount++;
            }

            rewardInstance.dailyReward = rewardTemplate; 
            rewardInstance.isClaimedToday = false;
            rewardInstance.Day = i;
            dailyRewards.Add(rewardInstance);
        }
    }

  
}

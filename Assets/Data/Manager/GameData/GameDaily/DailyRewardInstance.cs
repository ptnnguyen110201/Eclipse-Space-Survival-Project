using System;
using UnityEngine;

[Serializable]
public class DailyRewardInstance
{
    public DailyReward dailyReward;
    public int Day;
    public int Amount;
    public bool isClaimedToday;

    public bool CanClaim(DateTime today)
    {
        return today.Day == Day && !isClaimedToday;
    }
    public bool ClaimReward(PlayerData playerData, DateTime today)
    {
        if (!CanClaim(today))
        {
            Debug.LogWarning("Reward cannot be claimed today.");
            return false;
        }

        switch (dailyReward.dailyRewardType)
        {
            case DailyRewardType.Golds:
                playerData.currencyData.AddCurrency(playerData, Amount, CurrencyType.Golds);
                Debug.Log($"Added {Amount} Golds to player's balance.");
                break;

            case DailyRewardType.Diamonds:
                playerData.currencyData.AddCurrency(playerData, Amount, CurrencyType.Diamonds);
                Debug.Log($"Added {Amount} Diamonds to player's balance.");
                break;

            case DailyRewardType.Item:
                if (dailyReward.rewardItem != null && dailyReward.rewardItem.Count > 0)
                {



                    for (int i = 0; i < this.Amount; i++)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, dailyReward.rewardItem.Count);
                        ShipItemData randomItem = dailyReward.rewardItem[randomIndex] as ShipItemData;
                        playerData.shipItemEquipDatas.AddItem(randomItem);
                    }

                }
                else
                {
                    Debug.LogError("Reward item list is empty or null.");
                }
                break;

            default:
                Debug.LogError("Unknown reward type.");
                return false;
        }

        isClaimedToday = true;
        SaveSystem.SavePlayerData(playerData);
        return true;
    }
}

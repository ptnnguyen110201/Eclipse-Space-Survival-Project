using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyReward", menuName = "DailyCheckIn/DailyReward", order = 0)]
public class DailyReward : ScriptableObject
{
    public List<ScriptableObject> rewardItem;
    public Sprite rewardSprite;
    public DailyRewardType dailyRewardType;
}

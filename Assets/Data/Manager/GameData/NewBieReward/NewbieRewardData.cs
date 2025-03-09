using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewbieRewardData", menuName = "Rewards/NewbieRewardData")]
public class NewbieRewardData : ScriptableObject
{
    public List<NewbieReward> rewards = new List<NewbieReward>();
}

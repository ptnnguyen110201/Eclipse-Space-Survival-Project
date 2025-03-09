using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class NewbieRewardDatas
{
    public bool IsReward = false;
    public NewbieRewardData newbieRewardData;

    public void NewBieReward(PlayerData playerData)
    {
        if (IsReward == true) return;
        foreach(NewbieReward newbieReward in this.newbieRewardData.rewards) 
        {
            ShipItemData shipItemData = newbieReward.Item as ShipItemData;
            if (shipItemData == null) return;
            playerData.shipItemEquipDatas.AddItem(shipItemData);
        }
        this.IsReward = true;
        SaveSystem.SavePlayerData(playerData);
    }
 
}

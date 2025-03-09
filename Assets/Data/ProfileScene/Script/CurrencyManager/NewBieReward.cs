
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewBieReward : FuncManager 
{
    [SerializeField] protected List<Image> RewardImages;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRewardImage();
    }
    public void ShowUIImage(PlayerData playerData)
    {
        if (playerData == null) return;
        List<NewbieReward> rewards = playerData.newbieRewardDatas.newbieRewardData.rewards;
        for (int i = 0; i < RewardImages.Count; i++)
        {
            if (i >= rewards.Count)
            {
                RewardImages[i].gameObject.SetActive(false);
                continue;
            }

            this.RewardImages[i].gameObject.SetActive(true);
            ShipItemData shipItemData = rewards[i].Item as ShipItemData;
            if (shipItemData == null) continue;
            this.RewardImages[i].sprite = shipItemData.GetShipItemTierData().itemTierSprite;
        }
        playerData.newbieRewardDatas.NewBieReward(playerData);

        SaveSystem.SavePlayerData(playerData);
    }
    protected virtual void LoadRewardImage() 
    {
        if (this.RewardImages.Count > 0) return;
        foreach(Transform obj in this.transform.Find("Panel/RewardPanel")) 
        {
            Image image = obj.Find("Item_Image").GetComponent<Image>();
            this.RewardImages.Add(image);
        }
        Debug.Log(transform.name + " LoadRewardImage ", gameObject);
    }
}
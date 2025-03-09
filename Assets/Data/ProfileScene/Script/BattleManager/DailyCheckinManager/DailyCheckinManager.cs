using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
public class DailyCheckinManager : Spawner
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHolder();
    }
    protected override void Start()
    {
        base.Start();
        UIManager.Instance.CloseElement(this.gameObject);
    }
    protected override void LoadHolder()
    {
        this.holder = transform.Find("CheckingPage").GetComponent<Transform>();
    }
    public void OpenDailyChecking()
    {
        UIManager.Instance.OpenScaleUp(this.transform.gameObject, 0.5f);
        this.SpawnDailySlot();
    }
    public void CloseDailyChecking()
    {
        UIManager.Instance.CloseScaleUp(this.transform.gameObject, 0.5f);

    }
    protected virtual void SpawnDailySlot()
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        this.ClearItem();
        List<DailyRewardInstance> dailyRewardInstances = playerData.dailyCheckInData.dailyRewards;
        DateTime today = DateTime.Now;

        foreach (var dailyRewardInstance in dailyRewardInstances)
        {
            Transform slot = this.Spawn(this.prefabs[0], transform.position, transform.rotation);
            DailyCheckinSlot dailyCheckinSlot = slot.GetComponent<DailyCheckinSlot>();
            dailyCheckinSlot.ShowCheckinSlot(dailyRewardInstance);

            slot.localScale = Vector3.one;
            slot.gameObject.SetActive(true);
        }
    }

    private void ClearItem()
    {
        foreach (Transform item in this.holder)
        {
            this.Despawn(item);
        }

    }
}
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : Spawner
{

    protected override void Start()
    {
        base.Start();
        this.transform.gameObject.SetActive(false);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHolder();
    }
    protected override void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("QuestPage").GetComponent<Transform>();
        Debug.Log(transform.name + " Load Holder", gameObject);
    }
    private void SpawnQuest()
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        playerData.questDatas.CheckAndUpdateDailyQuests();
        if (playerData == null) return;
        for (int i = 0; i < playerData.questDatas.dailyQuests.Count; i++)
        {
            QuestInstance quest = playerData.questDatas.dailyQuests[i];
            Transform newQuest = this.Spawn(this.prefabs[0], transform.position, transform.rotation);
            newQuest.localScale = Vector3.one;

            QuestSlot questSlot = newQuest.GetComponent<QuestSlot>();
            questSlot.ShowUIQuest(quest);

            newQuest.gameObject.SetActive(true);
        }
    }
    public void OpenQuest()
    {
        this.SpawnQuest();
        UIManager.Instance.OpenScaleUp(transform.gameObject, 0.5f);

    }
    public void Close()
    {
        UIManager.Instance.CloseScaleUp(transform.gameObject, 0.5f);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.ClearQuest() ;
    }
    private void ClearQuest()
    {
        foreach (Transform obj in this.holder)
        {
            if (obj == null) continue;
            this.Despawn(obj);
        }
    }
}
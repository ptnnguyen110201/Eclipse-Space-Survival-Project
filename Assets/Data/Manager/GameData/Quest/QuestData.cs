using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class QuestData
{
    public List<QuestInstance> dailyQuests;
    public string lastUpdateDate;

    private void SetRandomQuest()
    {
        this.dailyQuests = this.RandomQuestInstances();
        this.lastUpdateDate = DateTime.Now.ToString("yyyy-MM-dd");
    }

    private List<QuestInstance> RandomQuestInstances(int count = 5)
    {
        Quest[] allQuests = Resources.LoadAll<Quest>("Quest");
        List<QuestInstance> questInstances = new List<QuestInstance>();
        List<int> usedIndexes = new List<int>();

        while (questInstances.Count < count)
        {
            int randomIndex = UnityEngine.Random.Range(0, allQuests.Length);

            if (!usedIndexes.Contains(randomIndex))
            {
                QuestInstance newQuestInstance = new QuestInstance(allQuests[randomIndex]);
                questInstances.Add(newQuestInstance);
                usedIndexes.Add(randomIndex);
            }
        }

        return questInstances;
    }

    public void UpdateQuestProgress(QuestType questType, int amount)
    {
        foreach (var questInstance in dailyQuests)
        {
            if (questInstance.baseQuest.questType == questType)
            {
                questInstance.UpdateProgress(amount);
            }
        }
    }

    public void CheckAndUpdateDailyQuests()
    {
        if (string.IsNullOrEmpty(lastUpdateDate))
        {
            SetRandomQuest();

            return;
        }

        DateTime lastUpdate;
        if (DateTime.TryParse(lastUpdateDate, out lastUpdate))
        {
            if ((DateTime.Now - lastUpdate).TotalDays >= 1)
            {
                Debug.Log("Updating daily quests for a new day.");
                SetRandomQuest();
            }
        }
        else
        {
            Debug.LogWarning("Invalid last update date. Generating new quests.");
            SetRandomQuest();
        }
    }
}

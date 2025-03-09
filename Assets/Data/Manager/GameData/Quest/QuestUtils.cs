using UnityEngine;
using System.Collections.Generic;

public static class QuestUtils
{
    private static readonly Dictionary<QuestType, string> questTypeDescriptions = new Dictionary<QuestType, string>
    {
        { QuestType.EearningGolds, "Earn Gold" },
        { QuestType.KillEnemies, "Defeat Enemies" },
        { QuestType.ItemLevelUp, "Upgrade Items" },
        { QuestType.PlayTotalGames, "Play Games" },
        { QuestType.SpeenGolds, "Spend Gold" },
        { QuestType.PlayTime, "Play Time (Minutes)" }
    };

    public static string GetQuestDescription(QuestType questType)
    {
        return questTypeDescriptions.TryGetValue(questType, out string description) ? description : questType.ToString();
    }
}

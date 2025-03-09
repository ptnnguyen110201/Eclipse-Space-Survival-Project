using System;
using System.Collections.Generic;

public enum GameEventType
{
    LevelUp,
    AddExp,
    BossDead,
    KilledEnemy,
    Win
}

public static class GameEvents
{
    public static readonly Dictionary<GameEventType, Action> eventMap = new Dictionary<GameEventType, Action>
    {
        { GameEventType.LevelUp, null },
        { GameEventType.AddExp, null },
        { GameEventType.BossDead, null },
        { GameEventType.KilledEnemy, null },
        { GameEventType.Win, null }
    };

    public static void Subscribe(GameEventType eventType, Action listener)
    {
        if (eventMap.ContainsKey(eventType))
            eventMap[eventType] += listener;
    }

    public static void Unsubscribe(GameEventType eventType, Action listener)
    {
        if (eventMap.ContainsKey(eventType))
            eventMap[eventType] -= listener;
    }

    public static void TriggerEvent(GameEventType eventType)
    {
        if (eventMap.ContainsKey(eventType))
            eventMap[eventType]?.Invoke();
    }
}

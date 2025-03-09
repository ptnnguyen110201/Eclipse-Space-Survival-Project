using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest", order = 1)]
[Serializable]
public class Quest : ScriptableObject
{
    public Sprite questIcon;
    public QuestType questType;   
    public int targetAmount;     
    public int currentAmount;     
    public PlayerGolds questReward;             
    public bool isCompleted;       
}
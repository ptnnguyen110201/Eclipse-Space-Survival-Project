using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipLevelMap", menuName = "ShipData/ShipLevelMap")]
public class ShipLevelMap : ScriptableObject
{
    public List<ShipLevelData> levels;
    protected void OnEnable()
    {
        this.GenerateLevelData();
    }
    public void GenerateLevelData()
    {
        levels = new List<ShipLevelData>();
        int baseEXP = 0;
        int incrementEXP = 10; 

        for (int i = 1; i <= 100; i++) 
        {
            ShipLevelData levelData = new ShipLevelData();
            levelData.level = i;

            if (i == 1)
            {
                levelData.requiredEXP = baseEXP; 
            }
            else
            {
                baseEXP += incrementEXP; 
                levelData.requiredEXP = baseEXP;
            }
            if (i % 10 == 0)
            {
                incrementEXP += 10;
            }

            levels.Add(levelData);
        }
    }
    public int GetLevel(double currentEXP)
    {
        int level = 0;
        foreach (var levelData in levels)
        {
            if (currentEXP >= levelData.requiredEXP)
            {
                level = levelData.level;

            }
            else
            {
                break;
            }
        }
        return level;
    }

    public double ExpNextLevel(int currentLevel)
    {
        foreach (ShipLevelData levelData in this.levels)
        {
            if (levelData.level == currentLevel + 1)
            {
                return levelData.requiredEXP;
            }
        }
        return -1;
    }

    
}

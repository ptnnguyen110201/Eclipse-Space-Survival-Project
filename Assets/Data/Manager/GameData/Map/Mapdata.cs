using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Mapdata", menuName = "Mapdata/Mapdata")]
public class Mapdata : ScriptableObject
{
    public int MapID;
    public string MapName;
    public Sprite MapBG;
    public EnergyData energyData;
    public List<Wave> MapWaves;

    public MapReward mapReward;
    public List<Sprite> mapEnemySprite;

    private void OnEnable()
    {
        LoadMapEnemySprites();
    }

    public virtual void LoadMapEnemySprites()
    {
        this.mapEnemySprite.Clear();

        EnemyData[] allEnemies = Resources.LoadAll<EnemyData>("Enemy");


        Dictionary<(EnemyID, ObjectType), EnemyData> enemyDataDictionary = new Dictionary<(EnemyID, ObjectType), EnemyData>();

        foreach (var enemy in allEnemies)
        {
            var key = (enemy.enemyID, enemy.objType);

            if (!enemyDataDictionary.ContainsKey(key))
            {
                enemyDataDictionary[key] = enemy;
            }
        }

        HashSet<Sprite> uniqueSprites = new HashSet<Sprite>();

        foreach (var wave in MapWaves)
        {
            foreach (var enemyWave in wave.EnemyWaves)
            {
                if (!enemyDataDictionary.TryGetValue((enemyWave.enemyID, ObjectType.Enemy), out EnemyData enemyData))
                {
                    continue;
                }

                ObjectAttribute attribute = enemyData.GetObjectAttribute(enemyWave.enemyType);
                if (attribute != null && attribute.objectSprite != null)
                {
                    uniqueSprites.Add(attribute.objectSprite);
                }
            }
        }
        mapEnemySprite = uniqueSprites.ToList();
    }



}

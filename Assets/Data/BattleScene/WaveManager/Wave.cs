using System;
using System.Collections.Generic;

[Serializable]
public class Wave
{
    public float spawnTimer;
    public int spawnDelay;
    public int spawnTime;
    public int maxCount;
    public bool isBossWave;
    public List<EnemySpawnInfo> EnemyWaves;
    public List<MinibossSpawnInfo> MiniBossWaves;
    public BossSpawnInfo BossWave;
    public bool warning = false;
}

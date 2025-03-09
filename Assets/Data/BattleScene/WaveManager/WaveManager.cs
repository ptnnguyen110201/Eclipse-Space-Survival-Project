using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : FuncManager
{
    [SerializeField] protected List<ObjectCtrl> activeEnemies = new List<ObjectCtrl>();
    [SerializeField] protected EnemySpawnRandom enemySpawnRandom;
    [SerializeField] protected BossCtrl activeBoss;
    [SerializeField] protected List<Wave> waves;
    [SerializeField] protected int currentWave = 0;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemySpawnRandom();
    }

    protected virtual void LoadEnemySpawnRandom()
    {
        if (this.enemySpawnRandom != null) return;
        this.enemySpawnRandom = transform.GetComponentInChildren<EnemySpawnRandom>();
        Debug.Log(transform.name + " LoadEnemySpawnRandom", gameObject);
    }


    public void AddEnemyToWave(ObjectCtrl objectCtrl)
    {
        if (objectCtrl == null || this.activeEnemies.Contains(objectCtrl)) return;
        if (objectCtrl.GetObjectData().objType == ObjectType.Boss)
        {
            BossCtrl bossCtrl = objectCtrl as BossCtrl;
            this.activeBoss = bossCtrl;
            this.activeBoss.OnBossDeath += this.HandleBossDead;
            return;
        }

        this.activeEnemies.Add(objectCtrl);
    }

    protected override void Start()
    {
        base.Start();
        this.StartSpawn();
        ShipManager.Instance.OnShipDead += HandleShipDead;

    }

    protected virtual void StartSpawn()
    {
        var mapData = GameManager.Instance.GetPlayerData().gameState.SelectedMapData.mapdata;
        if (mapData == null || mapData.MapWaves == null || mapData.MapWaves.Count == 0)
        {
            Debug.LogError("Invalid MapData or Waves are missing.");
            return;
        }

        this.waves = mapData.MapWaves;
        this.StartCoroutine(SpawnAllWaves(waves));
    }
    protected virtual IEnumerator SpawnAllWaves(List<Wave> waves)
    {
        yield return this.StartCoroutine(WarningManager.Instance.OnStartGame());
        while (this.currentWave < waves.Count)
        {
            Wave wave = waves[this.currentWave];
            MapStatistics.Instance.GameTimerStart(wave.spawnDelay + wave.spawnTime);
            this.enemySpawnRandom.StartWaveSpawning(wave);
            yield return new WaitForSeconds(wave.spawnTime);
            if (this.currentWave + 1 < waves.Count)
            {
                Wave nextWave = waves[this.currentWave + 1];
                WarningManager.Instance.OnWarning(nextWave.warning, nextWave.isBossWave);
                yield return new WaitForSeconds(wave.spawnDelay); 
                WarningManager.Instance.OnWarning(false); 
            }
            this.currentWave++;
        }
    }


    private void HandleShipDead()
    {
        bool CanRevive = MapStatistics.Instance.GetMapStatisticsData().CanRevive() ;
        if (CanRevive) 
        {
            MenuManager.Instance.OpenReviveMenu();
            return;
        }
        this.StartCoroutine(this.DelayedTriggerGameEnd(false)); 
    }

    private void HandleBossDead()
    {
        if(this.activeBoss != null) 
        {
            this.activeBoss.OnBossDeath -= HandleBossDead;
        }
        if (this.IsVictoryConditionMet())
        {
         
            this.StartCoroutine(DelayedTriggerGameEnd(true));
        }
      
    }

    private IEnumerator DelayedTriggerGameEnd(bool isWin)
    {
        yield return new WaitForSeconds(3f); 
        GameManager.Instance.TriggerGameEnd(isWin);
    }

    public void ClearEnemy()
    {
        foreach (ObjectCtrl objectCtrl in this.activeEnemies)
        {
            EnemyCtrl enemyCtrl = objectCtrl as EnemyCtrl;
            EnemySpawner.Instance.Despawn(enemyCtrl.transform);
            Debug.Log(this.activeEnemies.Count);
        }
    }
    protected bool IsPlayerDefeated()
    {
        ShipCtrl shipCtrl = ShipManager.Instance.GetShipCtrl();
        return shipCtrl == null || !shipCtrl.gameObject.activeSelf;
    }

    protected bool IsVictoryConditionMet()
    {
        return this.currentWave >= this.waves.Count;
    }
    private void OnDestroy()
    {
        ShipManager.Instance.OnShipDead -= HandleShipDead;
      
    }
}

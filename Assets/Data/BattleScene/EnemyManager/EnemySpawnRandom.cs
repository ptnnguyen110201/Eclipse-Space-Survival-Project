using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnRandom : FuncManager
{
    [Header("Spawner Random")]
    [SerializeField] protected WaveManager waveManager;
    [SerializeField] protected EnemySpawner enemySpawner;
    [SerializeField] protected EnemySpawnPoint enemySpawnPoint;
    [SerializeField] protected BossSpawnPoint bossSpawnPoint;
    [SerializeField] protected float spawnTimer;
    [SerializeField] protected int maxSpawnCount;
    [SerializeField] protected Coroutine currentSpawnCoroutine;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemySpawner();
        this.LoadEnemySpawnPoint();
        this.LoadWaveManager();
    }
    public virtual void StopWaveSpawning()
    {
        if (this.currentSpawnCoroutine != null)
        {
            this.StopCoroutine(this.currentSpawnCoroutine);
        }
    }
    public virtual void StartWaveSpawning(Wave currentWave)
    {
        if (this.currentSpawnCoroutine != null)
        {
            this.StopCoroutine(this.currentSpawnCoroutine);
        }
        this.maxSpawnCount = currentWave.maxCount;
        this.spawnTimer = currentWave.spawnTimer;
        this.currentSpawnCoroutine = StartCoroutine(this.SpawnEnemiesByWave(currentWave));
    }


    private IEnumerator SpawnEnemiesByWave(Wave currentWave)
    {

        if (currentWave.isBossWave && currentWave.BossWave != null)
        {
            Transform bossPoint = this.bossSpawnPoint.GetRandom();
            this.SpawnBoss(currentWave.BossWave, bossPoint);
            yield break;
        }

        if (currentWave.MiniBossWaves.Count > 0)
        {
            foreach (MinibossSpawnInfo miniboss in currentWave.MiniBossWaves)
            {
                Transform spawnPoint = this.enemySpawnPoint.GetRandom();
                this.SpawnEnemy(miniboss, spawnPoint);
            }
        }

        while (true)
        {
            yield return new WaitForSeconds(currentWave.spawnTimer);
            if (this.SpawnLimit())
            {
                EnemySpawnInfo waveEnemy = this.GetEnemyByRatio(currentWave.EnemyWaves);
                if (waveEnemy != null)
                {
                    Transform spawnPoint = this.enemySpawnPoint.GetRandom();
                    this.SpawnEnemy(waveEnemy, spawnPoint);
                }
            }



        }
    }


    private void SpawnBoss(BossSpawnInfo bossWave, Transform spawnPoints)
    {
        this.waveManager.ClearEnemy();
        this.SpawnEnemy(bossWave, spawnPoints);
        BossHPBarUI.Instance.gameObject.SetActive(true);

    }

    private void SpawnEnemy(object spawnInfo, Transform spawnPoint)
    {
        if (spawnInfo == null || spawnPoint == null) return;

        Transform newEnemy = null;

        if (spawnInfo is EnemySpawnInfo enemyWave)
        {
            newEnemy = this.enemySpawner.SpawnEnemy(enemyWave.enemyID, spawnPoint.position, Quaternion.identity);
            ObjectCtrl objectCtrl = newEnemy.GetComponent<ObjectCtrl>();
            objectCtrl.SetObjectAttribute(enemyWave.enemyType);
            this.waveManager.AddEnemyToWave(objectCtrl);

        }
        else if (spawnInfo is MinibossSpawnInfo minibossWave)
        {
            newEnemy = this.enemySpawner.SpawnEnemy(minibossWave.enemyID, spawnPoint.position, Quaternion.identity);
            ObjectCtrl objectCtrl = newEnemy.GetComponent<ObjectCtrl>();
            objectCtrl.SetObjectAttribute(minibossWave.enemyType);
            this.waveManager.AddEnemyToWave(objectCtrl);


        }
        else if (spawnInfo is BossSpawnInfo bossSpawnInfo)
        {
            newEnemy = this.enemySpawner.SpawnEnemy(bossSpawnInfo.enemyID, spawnPoint.position, Quaternion.identity);
            ObjectCtrl objectCtrl = newEnemy.GetComponent<ObjectCtrl>();
            objectCtrl.SetObjectAttribute(bossSpawnInfo.enemyType);
            this.waveManager.AddEnemyToWave(objectCtrl);
            BossCtrl bossCtrl = objectCtrl as BossCtrl;
            if (bossCtrl != null)
            {
                BossHPBarUI.Instance.SetBossCtrl(bossCtrl);

            }
        }
        if (newEnemy == null)
        {
            Debug.LogError("Failed to spawn enemy. Transform is null.");
            return;
        }

        newEnemy.gameObject.SetActive(true);


    }


    protected virtual EnemySpawnInfo GetEnemyByRatio(List<EnemySpawnInfo> enemyWaves)
    {
        if (enemyWaves == null || enemyWaves.Count == 0) return null;

        float totalRatio = 0f;
        foreach (EnemySpawnInfo waveEnemy in enemyWaves)
        {
            totalRatio += waveEnemy.spawnRatio;
        }

        float randomValue = Random.Range(0f, totalRatio);
        float cumulativeRatio = 0f;

        foreach (EnemySpawnInfo waveEnemy in enemyWaves)
        {
            cumulativeRatio += waveEnemy.spawnRatio;
            if (randomValue <= cumulativeRatio)
            {
                return waveEnemy;
            }
        }

        return null;
    }

    private bool SpawnLimit() => this.maxSpawnCount > this.enemySpawner.SpawnedCount;


    protected virtual void LoadEnemySpawner()
    {
        if (this.enemySpawner != null) return;
        this.enemySpawner = transform.GetComponentInChildren<EnemySpawner>();
        Debug.LogWarning(transform.name + ": Load EnemySpawner", gameObject);
    }

    protected virtual void LoadEnemySpawnPoint()
    {
        if (this.enemySpawnPoint != null) return;
        this.enemySpawnPoint = transform.GetComponentInChildren<EnemySpawnPoint>();
        this.bossSpawnPoint = transform.GetComponentInChildren<BossSpawnPoint>();
        Debug.LogWarning(transform.name + ": Load EnemySpawnPoint", gameObject);
    }

    protected virtual void LoadWaveManager()
    {
        if (this.waveManager != null) return;
        this.waveManager = transform.GetComponentInParent<WaveManager>();
        Debug.LogWarning(transform.name + ": Load WaveManager", gameObject);
    }
}

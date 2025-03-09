using UnityEngine;

public class EnemySpawner : Spawner
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance => instance;
    protected override void Awake()
    {
        if (EnemySpawner.instance != null) Debug.LogError("Only 1 EnemySpawner allow to exist");
        EnemySpawner.instance = this;
    }

    public virtual Transform SpawnEnemy(EnemyID enemyID, Vector3 Pos, Quaternion Rot)
    {
        Transform newEnemy = this.Spawn(enemyID.ToString(), Pos, Rot);
        return newEnemy;
    }

    public virtual void ClearEnemies() 
    {
        foreach(Transform obj in this.holder) 
        {
            if (obj == null) continue;
            this.Despawn(obj);
        }
    }
}

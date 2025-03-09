using UnityEngine;

public class FxSpawner : Spawner
{
    private static FxSpawner instance;
    public static FxSpawner Instance => instance;
    protected override void Awake()
    {
        if (FxSpawner.instance != null) Debug.LogError("Only 1 FxSpawner allow to exist");
        FxSpawner.instance = this;
    }

    public virtual Transform SpawnFx(FxType fxType, Vector3 Pos, Quaternion Rot)
    {
        Transform newEnemy = this.Spawn(fxType.ToString(), Pos, Rot);
        return newEnemy;
    }

}

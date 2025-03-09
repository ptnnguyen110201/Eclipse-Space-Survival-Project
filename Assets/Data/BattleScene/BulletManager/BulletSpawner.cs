
using UnityEngine;


public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance => instance;
    protected override void Awake()
    {
        if (BulletSpawner.instance != null) Debug.LogError("Only 1 BulletSpawner allow to exist");
        BulletSpawner.instance = this;
    }

    public virtual Transform SpawnBullet(BulletType bulletType, Vector3 Pos, Quaternion Rot) 
    {
        if(bulletType == BulletType.None) 
        {
            Debug.Log("BulletType is None");
            return null;
        }
        Transform newBullet = this.Spawn(bulletType.ToString(), Pos, Rot);
        newBullet.transform.gameObject.SetActive(true);
        return newBullet;
    }


}

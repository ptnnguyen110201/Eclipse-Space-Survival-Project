using UnityEngine;

public class ShipSpawner : Spawner
{
    public virtual Transform SpawnShip()
    {
        Vector3 Pos = transform.position;
        Quaternion Rot = Quaternion.identity;
        Transform newBG = this.Spawn(this.prefabs[0], Pos, Rot);
        newBG.gameObject.SetActive(true);
        return newBG;
    }
    public virtual Transform Respawn(Vector3 Pos)
    {
        Quaternion Rot = Quaternion.identity;
        Transform newBG = this.Spawn(this.prefabs[0], Pos, Rot);
        newBG.gameObject.SetActive(true);
        return newBG;
    }
}

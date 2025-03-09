

public class EnemyDespawnByDis : DespawnByDistance
{
    public override void DespawnObject()
    {
        EnemySpawner.Instance.Despawn(transform.parent);
    }

}

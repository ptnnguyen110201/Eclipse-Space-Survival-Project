


public class ShipFxDespawnByTime : DespawnByTime
{
    public override void DespawnObject()
    {
        FxSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = 0.5f;
    }

    
}

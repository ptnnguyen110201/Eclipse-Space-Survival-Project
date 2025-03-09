

public class RPGDamageSender : ShipAOEDamageSender
{
    public override void DestroyObject()
    {

        BulletSpawner.Instance.Despawn(transform.parent);
        AudioManager.Instance.SpawnSFX(SoundCode.RPGSound);
        base.DestroyObject();

    }
    protected override void FxBullet(FxType fxType)
    {
        base.FxBullet(FxType.RPG);
    }
}

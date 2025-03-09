

public class RapidFireDamageSender : ShipSingleDamageSender
{
    public override void Send(DamageReceiver damageReceiver)
    {
        base.Send(damageReceiver);
        base.DestroyObject();
    }
    protected override void FxBullet(FxType fxType)
    {
        base.FxBullet(FxType.RapidFire);
    }
}

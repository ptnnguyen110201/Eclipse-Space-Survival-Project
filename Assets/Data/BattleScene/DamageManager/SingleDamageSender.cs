
public class SingleDamageSender : DamageSendBase
{
    public override void Send(DamageReceiver damageReceiver)
    {
        base.Send(damageReceiver);
    }
    public override void DestroyObject()
    {
        BulletSpawner.Instance.Despawn(transform.parent);
    }
}

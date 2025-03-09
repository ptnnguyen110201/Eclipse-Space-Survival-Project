using System.Collections;
using UnityEngine;

public class BoomImpart : ItemImpart
{

    public override IEnumerator FlyOutAndReturn(Transform returnPos)
    {
        yield return base.FlyOutAndReturn(returnPos);
        this.FxImpact();
        BoomSender boomSender = this.itemCtrl.GetItemSender() as BoomSender;
        boomSender.TriggerAOEDamage();

    }
    protected void FxImpact()
    {
        Transform newFx = FxSpawner.Instance.SpawnFx(FxType.Boom, transform.position, transform.rotation);
        newFx.gameObject.SetActive(true);
    }
}

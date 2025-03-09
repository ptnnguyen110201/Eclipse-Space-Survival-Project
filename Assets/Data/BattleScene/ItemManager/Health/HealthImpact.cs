using System.Collections;
using UnityEngine;

public class HealthImpact : ItemImpart
{

    public override IEnumerator FlyOutAndReturn(Transform returnPos)
    {
        yield return base.FlyOutAndReturn(returnPos);
        this.itemCtrl.GetItemSender().Send(returnPos);
    }

}

using System.Collections;
using UnityEngine;

public class GoldsImpact : ItemImpart
{
    public override IEnumerator FlyOutAndReturn(Transform returnPos)
    {
        yield return base.FlyOutAndReturn(returnPos);
        this.itemCtrl.GetItemSender().Send(returnPos);
    }

}

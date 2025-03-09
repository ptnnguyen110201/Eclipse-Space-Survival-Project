using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipItemMergeBtn : ButtonBase
{
    protected override void OnClick()
    {
        ShipItemMergeManager.Instance.OpenItemMergedStat();
    }
}

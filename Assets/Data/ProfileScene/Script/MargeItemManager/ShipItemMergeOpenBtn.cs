using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipItemMergeOpenBtn : ButtonBase
{
    protected override void OnClick()
    {
        ShipItemMergeManager.Instance.OpenMergeBar();
    }
}

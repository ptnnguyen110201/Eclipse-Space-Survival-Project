using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipItemMergeCloseBtn : ButtonBase
{
    protected override void OnClick()
    {
        EquipmentBarManager.Instance.OpenEquipmentBar();
    }
}

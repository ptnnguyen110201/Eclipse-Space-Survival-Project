using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentOpenBtn : ButtonBase
{
    protected override void OnClick()
    {
        EquipmentBarManager.Instance.OpenEquipmentBar();
    }
}

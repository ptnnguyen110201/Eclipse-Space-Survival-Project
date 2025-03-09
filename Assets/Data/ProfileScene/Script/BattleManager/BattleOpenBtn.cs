using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOpenBtn : ButtonBase
{
    protected override void OnClick()
    {
        BattleManager.Instance.OpenBattle();
    }
}

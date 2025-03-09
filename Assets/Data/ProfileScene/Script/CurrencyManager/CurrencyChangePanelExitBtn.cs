using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanelExitBtn : ButtonBase
{

    protected override void OnClick()
    {
        CurrencyManager.Instance.CloseChangeCurrenCy();
    }
}

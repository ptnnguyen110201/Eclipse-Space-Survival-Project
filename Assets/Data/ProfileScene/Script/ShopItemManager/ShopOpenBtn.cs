using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpenBtn : ButtonBase
{
    protected override void OnClick()
    {
        ShopBarManager.Instance.OpenShop();
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class UISelectableRandomBtn : ButtonBase
{
    [SerializeField] protected bool RandomBtn = false;
    protected override void OnClick()
    {
        if (this.RandomBtn) return;
        this.RandomBtn = true;
        UiSelectableBar.Instance.OpenItemBar();
        if (this.RandomBtn)
            transform.gameObject.SetActive(false);

    }
}
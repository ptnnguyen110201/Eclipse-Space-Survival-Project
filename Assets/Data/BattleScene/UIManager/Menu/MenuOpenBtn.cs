using UnityEngine;
public class MenuOpenBtn : ButtonBase
{
    protected override void OnClick()
    {
        MenuManager.Instance.OpenMenu();
    }
}
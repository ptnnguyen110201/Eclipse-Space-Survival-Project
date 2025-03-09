using UnityEngine;
public class MenuCloseBtn : ButtonBase
{
    protected override void OnClick()
    {
        MenuManager.Instance.CloseMenu();
    }
}
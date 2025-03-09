using UnityEngine;
public class SettingCloseBtn : ButtonBase
{
    protected override void OnClick()
    {
        BattleManager.Instance.GetSettingPanel().CloseSettings();
    }
}
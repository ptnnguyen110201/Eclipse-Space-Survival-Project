using UnityEngine;
public class SettingOpenBtn : ButtonBase
{
    protected override void OnClick()
    {
        BattleManager.Instance.GetSettingPanel().OpenSettings();
    }
}
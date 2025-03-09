using UnityEngine;
public class DailyCheckinCloseBtn : ButtonBase
{
    protected override void OnClick()
    {
        BattleManager.Instance.GetDailyCheckingManager().CloseDailyChecking();
    }
}
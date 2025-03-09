using UnityEngine;
public class DailyCheckinOpenbtn : ButtonBase
{
    protected override void OnClick()
    {
        BattleManager.Instance.GetDailyCheckingManager().OpenDailyChecking();
    }
}
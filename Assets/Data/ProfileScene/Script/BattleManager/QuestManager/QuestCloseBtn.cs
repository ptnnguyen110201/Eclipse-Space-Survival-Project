using UnityEngine;
public class QuestCloseBtn : ButtonBase
{
    protected override void OnClick()
    {
        BattleManager.Instance.GetQuestManager().Close();
    }
}
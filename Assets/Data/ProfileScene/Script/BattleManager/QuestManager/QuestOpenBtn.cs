using UnityEngine;
public class QuestOpenBtn : ButtonBase
{
    protected override void OnClick()
    {
        BattleManager.Instance.GetQuestManager().OpenQuest();
    }
}
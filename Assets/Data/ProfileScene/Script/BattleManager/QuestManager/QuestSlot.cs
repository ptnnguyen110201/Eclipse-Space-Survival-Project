using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class QuestSlot : FuncManager
{
    [SerializeField] protected QuestClaimBtn questClaimBtn;
    [SerializeField] protected TextMeshProUGUI questName;
    [SerializeField] protected TextMeshProUGUI questValue;
    [SerializeField] protected TextMeshProUGUI questReward;
    [SerializeField] protected Image questIcon;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadQuestClaimBtn();
        this.LoadQuestName();
        this.LoadQuestValue();
        this.LoadQuestReward();
        this.LoadQuestIcon();
    }
    public void ShowUIQuest(QuestInstance questInstance)
    {
        this.questName.text = QuestUtils.GetQuestDescription(questInstance.baseQuest.questType);
        this.questValue.text =  $"{this.FormatNumber(questInstance.currentAmount)} / " +
                                $"{this.FormatNumber(questInstance.baseQuest.targetAmount)}";
        this.questReward.text = $"{this.FormatNumber(questInstance.baseQuest.questReward.Amount)}";
        this.questIcon.sprite = questInstance.baseQuest.questIcon;
        this.questClaimBtn.SetState(questInstance);
    }
    protected virtual void LoadQuestClaimBtn()
    {
        if (this.questClaimBtn != null) return;
        this.questClaimBtn = transform.GetComponentInChildren<QuestClaimBtn>();
        Debug.Log(transform.name + " LoadQuestClaimBtn ", gameObject);
    }

    protected virtual void LoadQuestName()
    {
        if (this.questName != null) return;
        this.questName = transform.Find("QuestName").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadQuestName ", gameObject);
    }
    protected virtual void LoadQuestValue()
    {
        if (this.questValue != null) return;
        this.questValue = transform.Find("QuestValue").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadQuestValue ", gameObject);
    }
    protected virtual void LoadQuestReward()
    {
        if (this.questReward != null) return;
        this.questReward = transform.Find("QuestReward").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " LoadQuestReward ", gameObject);
    }
    protected virtual void LoadQuestIcon()
    {
        if (this.questIcon != null) return;
        this.questIcon = transform.Find("QuestIcon/Icon").GetComponent<Image>();
        Debug.Log(transform.name + " LoadQuestIcon ", gameObject);
    }


    protected string FormatNumber(int value) => NumberFormatter.FormatNumber(value);
}
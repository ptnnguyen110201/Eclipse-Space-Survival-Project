using TMPro;
using UnityEngine;

public class QuestClaimBtn : ButtonBase
{
    [SerializeField] protected Transform claimedIcon;
    [SerializeField] protected TextMeshProUGUI TextState;
    [SerializeField] protected QuestInstance quest;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextState();
        this.LoadClaimedIcon();
    }
    protected virtual void LoadTextState()
    {
        if (this.TextState != null) return;
        this.TextState = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + "LoadTextState ", gameObject);
    }

    protected virtual void LoadClaimedIcon()
    {
        if (this.claimedIcon != null) return;
        this.claimedIcon = transform.Find("Claimed").GetComponent<Transform>();
        Debug.Log(transform.name + "LoadTextState ", gameObject);
    }
    public void SetState(QuestInstance questInstance)
    {
        this.quest = questInstance;

        if (questInstance.isRewardClaimed)
        {
            this.TextState.gameObject.SetActive(false);
            this.claimedIcon.gameObject.SetActive(true);
            this.button.interactable = false;
        }
        else if (questInstance.isCompleted)
        {
            this.TextState.text = "Claim";
            this.TextState.gameObject.SetActive(true);

            this.button.interactable = true;
            this.claimedIcon.gameObject.SetActive(false);
        }
        else
        {
            this.TextState.text = "Claim";
            this.TextState.gameObject.SetActive(true);
            this.button.interactable = false;
            this.claimedIcon.gameObject.SetActive(false);
        }
    }
    protected override void OnClick()
    {
        quest.AddReward(PlayerDataLoad.Instance.GetPlayerData());
        SetState(this.quest);
    }
}
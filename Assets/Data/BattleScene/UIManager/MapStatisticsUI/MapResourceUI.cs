using TMPro;
using UnityEngine;

public class MapResourceUI : FuncManager
{
    [SerializeField] protected TextMeshProUGUI killsText;
    [SerializeField] protected TextMeshProUGUI earningsText;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadKillsText();
        this.LoadEarningsText();
    }
    
    protected virtual void LoadKillsText() 
    {
        if (this.killsText != null) return;
        this.killsText = transform.Find("Killed").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + "Load KillsText ", gameObject);
    }
    protected virtual void LoadEarningsText()
    {
        if (this.earningsText != null) return;
        this.earningsText = transform.Find("Golds").GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + "Load KillsText ", gameObject);

    }

    public void UpdateKillsUI(int totalKills)
    {
        if (totalKills < 0) return;
        this.killsText.text = $"{totalKills}";
    }

    public void UpdateEarningsUI(int totalEarnings)
    { 
        if (totalEarnings < 0) return;
        this.earningsText.text = $"{totalEarnings}";
    }
}

using TMPro;
using UnityEngine;

public class MapTimerUI : FuncManager
{
    [SerializeField] protected TextMeshProUGUI TimerText;



    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTimerText();
    }

  
    protected virtual void LoadTimerText()
    {
        if (this.TimerText != null) return;
        this.TimerText = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + "Load TimerText", gameObject);

    }

    public virtual void SetTimer(int totalTimer)
    {
        if (this.TimerText != null)
        {
            int minutes = totalTimer / 60; 
            int seconds = totalTimer % 60; 

            this.TimerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }
    }

}

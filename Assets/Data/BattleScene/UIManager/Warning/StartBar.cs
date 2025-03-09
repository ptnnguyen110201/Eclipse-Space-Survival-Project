using System;
using TMPro;
using UnityEngine;

public class StartBar : FuncManager
{
    [SerializeField] protected TextMeshProUGUI Text;


    protected virtual void LoadText()
    {
        if (this.Text != null) return;
        this.Text = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + " Load Text", gameObject);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }
    public virtual void SetStartText(string Text)
    {
        if (this.Text.text == string.Empty) return;
        this.Text.text = Text;
    }

}

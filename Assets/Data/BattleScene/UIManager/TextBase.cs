
using TMPro;
using UnityEngine;

public abstract class TextBase : FuncManager
{
    [Header("TextBase")]
    [SerializeField] protected TextMeshProUGUI Text;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextMeshProUGUI();
    }

    protected virtual void LoadTextMeshProUGUI() 
    {
        if(this.Text != null) return;
        this.Text = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(transform.name + "LoadTextMeshProUGUI", gameObject);
    }
    
}

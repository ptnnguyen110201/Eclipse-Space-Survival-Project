using TMPro;
using UnityEngine;
using DG.Tweening;

public class FxDamagePopup : FuncManager
{
    [SerializeField] protected TextMeshPro Text;
    [SerializeField] protected float currentFont;
    [SerializeField] protected Color normalColor = Color.white;
    [SerializeField] protected Color criticalColor = Color.red;
    [SerializeField] protected float popupDuration = 0.75f; 
    [SerializeField] protected Vector3 popupOffset = new Vector3(0, 0.5f, 0); 
    [SerializeField] protected float fadeDuration = 0.375f; 

    private Sequence popupSequence;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextMeshPro();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.Text.fontSize = this.currentFont;
        this.PlayPopupAnimation();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (popupSequence != null && popupSequence.IsActive())
        {
            popupSequence.Kill();
        }
    }

    protected virtual void LoadTextMeshPro()
    {
        if (this.Text != null) return;
        this.Text = transform.GetComponentInChildren<TextMeshPro>();
        this.currentFont = this.Text.fontSize;
        Debug.Log(transform.name + "LoadTextMeshPro", gameObject);
    }

    public void SetDamage(double Damage, bool isCritical, float CritDamage)
    {
        if (this.Text == null) return;
        this.Text.SetText(((int)Damage).ToString());
        if (isCritical)
        {
            CritDamage = Mathf.Clamp(CritDamage, 1.0f, 2.0f);
            Color damageColor;

            if (CritDamage < 1.1f)
            {
                damageColor = Color.yellow;
            }
            else if (CritDamage >= 1.1f && CritDamage <= 1.5f)
            {
                damageColor = new Color(1f, 0.647f, 0f);
            }
            else
            {
                damageColor = Color.red;
            }

            this.Text.color = damageColor;
            this.Text.fontSize = this.currentFont * CritDamage;
        }
        else
        {
            this.Text.color = normalColor;
            this.Text.fontSize = this.currentFont;
        }
    }

    private void PlayPopupAnimation()
    {
        this.Text.color = new Color(this.Text.color.r, this.Text.color.g, this.Text.color.b, 1f);

        popupSequence = DOTween.Sequence();
        popupSequence.Append(transform.parent.DOMove(transform.parent.position + popupOffset, popupDuration).SetEase(Ease.OutQuad));
        popupSequence.Append(this.Text.DOFade(0, fadeDuration));
    }
}

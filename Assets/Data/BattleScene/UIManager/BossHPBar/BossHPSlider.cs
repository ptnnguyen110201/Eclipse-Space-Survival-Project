using DG.Tweening;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using TMPro;

public class BossHPSlider : SliderBase
{
    [SerializeField] protected TextMeshProUGUI Percent;  
    [SerializeField] protected double maxValue;        
    [SerializeField] protected double currentValue;     

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHPPercent();  
    }

    protected virtual void LoadHPPercent()
    {
        if (this.Percent != null) return;  
        this.Percent = transform.GetComponentInChildren<TextMeshProUGUI>(); 
        Debug.LogWarning(transform.name + ": LoadSlider", gameObject); 
    }

    private void UpdateSlider()
    {
        if (this.maxValue == 0)
        {
            this.slider.DOValue(0, 0.25f).SetUpdate(true); 
            if (this.Percent != null) this.Percent.text = "0%";  
            return;
        }

        double valuePercent = this.currentValue / this.maxValue;  
        float newValue = (float)valuePercent;

        this.slider.DOValue(newValue, 0.25f).SetUpdate(true);  
        if (this.Percent != null)
        {
            this.Percent.text = Mathf.RoundToInt((float)(valuePercent * 100)) + "%"; 
        }
    }

    public virtual void SetMaxValue(double maxValue)
    {
        this.maxValue = maxValue;  
        this.UpdateSlider();        
    }

    public virtual void SetCurrentValue(double currentValue)
    {
        this.currentValue = currentValue;  
        this.UpdateSlider();               
    }
    protected override void OnChanged(float newValue)
    {
    
    }
}

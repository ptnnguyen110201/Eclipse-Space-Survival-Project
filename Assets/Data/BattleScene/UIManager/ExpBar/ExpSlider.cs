using DG.Tweening;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class ExpSlider : SliderBase
{
    [SerializeField] protected double maxValue;
    [SerializeField] protected double currentValue;
    private void UpdateSlider()
    {
        if (this.maxValue == 0)
        {
            this.slider.DOValue(0, 0.25f).SetUpdate(true);
            return;
        }

        double valuePercent = this.currentValue / this.maxValue;
        float newValue = (float)valuePercent;
        this.slider.DOValue(newValue, 0.25f).SetUpdate(true);
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

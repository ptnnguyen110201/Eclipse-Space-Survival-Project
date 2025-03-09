using UnityEngine;
using System.Collections.Generic;
using DG.Tweening; // Import DoTween namespace
using TMPro;

public class UIManager : FuncManager
{
    private static UIManager instance;
    public static UIManager Instance => instance;
    [SerializeField] protected List<GameObject> uiElements;

    protected override void Awake()
    {
        if (UIManager.instance != null) Debug.LogError("Only 1 UIManager allowed to exist");
        UIManager.instance = this;
    }




    public void CloseUIElementsExcept(GameObject excludeUI)
    {
        foreach (GameObject element in this.uiElements)
        {
            if (element == excludeUI) continue;
            this.CloseElement(element);
        }
    }

    public void OpenElement(GameObject element)
    {
        if (element.activeSelf) return;
        element.SetActive(true);
    }

    public void CloseElement(GameObject element)
    {
        if (!element.activeSelf) return;
        element.SetActive(false);
    }

    public void OpenScaleUp(GameObject element, float duration)
    {
        element.transform.localScale = Vector3.zero;
        element.SetActive(true);

        element.transform.DOScale(Vector3.one, duration).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void CloseScaleUp(GameObject element, float duration)
    {
        element.transform.DOScale(Vector3.zero, duration).SetEase(Ease.InBack).OnComplete(() =>
        {
            element.SetActive(false);
        });
    }

    public void ScaleAnimation(GameObject element, float targetScale, float duration)
    {
        Vector3 originalScale = Vector3.one;
        Vector3 targetScaleVector = new Vector3(targetScale, targetScale, targetScale);
        float halfDuration = duration / 2f;

        element.transform.DOScale(targetScaleVector, halfDuration)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                element.transform.DOScale(originalScale, halfDuration).SetEase(Ease.InSine);
            });
    }

    public void IncreaseValue(float startValue, float targetValue, float duration, TextMeshProUGUI textValue)
    {
        DOTween.To(() => startValue, x =>
        {
            startValue = x;
            textValue.text = NumberFormatter.FormatNumber(Mathf.RoundToInt(startValue));
        }, targetValue, duration).SetEase(Ease.Linear);
    }
    public void IncreaseValue(float startValue, float targetValue,float maxValue, float duration, TextMeshProUGUI textValue)
    {

        DOTween.To(() => startValue, x =>
        {
            startValue = x;
            textValue.text = $"{NumberFormatter.FormatNumber(Mathf.RoundToInt(startValue))} / {NumberFormatter.FormatNumber(Mathf.RoundToInt(maxValue))}";
        }, targetValue, duration).SetEase(Ease.Linear);
    }

}

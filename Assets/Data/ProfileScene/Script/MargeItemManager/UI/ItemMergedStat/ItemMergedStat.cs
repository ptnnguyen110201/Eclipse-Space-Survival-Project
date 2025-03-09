using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemMergedStat : ButtonBase
{
    [SerializeField] protected ItemMergedSlot itemMergedSlot;
    [SerializeField] protected ItemMergedDetailStat itemMergedDetailStat;
    [SerializeField] protected List<ItemMergeRequiredSlot> itemMergeRequiredSlots;
    [SerializeField] protected TextMeshProUGUI closeText;
    [SerializeField] protected float moveSpeed = 10;
    [SerializeField] protected float delayBetweenActions = 0.25f;

    protected override void OnEnable()
    {
        DisableButtonAndCloseText();
    }

    public void ShowUI(List<ShipItemEquipData> shipItemEquipDatas)
    {
        StopAllCoroutines();
        StartCoroutine(ShowUICoroutine(shipItemEquipDatas));
    }

    private IEnumerator ShowUICoroutine(List<ShipItemEquipData> shipItemEquipDatas)
    {
        if (shipItemEquipDatas.Count == 0) yield break;

        InitializeMergeUI(shipItemEquipDatas);
        yield return PerformStepWithDelay(MoveAndScaleItemsCoroutine(shipItemEquipDatas));
        yield return PerformStepWithDelay(MergeFinal(shipItemEquipDatas));
        yield return PerformStepWithDelay(itemMergedDetailStat.ShowUIWithDelay(shipItemEquipDatas[0]));
        yield return ActivateButtonAndCloseText();
    }

    private void InitializeMergeUI(List<ShipItemEquipData> shipItemEquipDatas)
    {
        itemMergedSlot.SetItemMergeUI(shipItemEquipDatas[0]);

        for (int i = 1; i < shipItemEquipDatas.Count; i++)
        {
            itemMergeRequiredSlots[i - 1].SetItemUI(shipItemEquipDatas[i], shipItemEquipDatas);
            itemMergeRequiredSlots[i - 1].gameObject.SetActive(true);
        }

        Canvas.ForceUpdateCanvases();
    }

    private IEnumerator PerformStepWithDelay(IEnumerator coroutine)
    {
        yield return new WaitForSeconds(delayBetweenActions);
        yield return StartCoroutine(coroutine);
    }

    private IEnumerator MoveAndScaleItemsCoroutine(List<ShipItemEquipData> shipItemEquipDatas)
    {
        Vector3 targetPosition = itemMergedSlot.GetComponent<RectTransform>().position;
        int itemCount = shipItemEquipDatas.Count - 1;
        SetActiveMergeRequiredSlots(itemCount);

        bool allItemsReached = false;
        Vector3 initialScale = Vector3.one;
        Vector3 targetScale = Vector3.zero;
        float minDistance = 0.01f;

    
        while (!allItemsReached)
        {
            allItemsReached = true;

            for (int i = 0; i < itemCount; i++)
            {
                RectTransform itemRect = itemMergeRequiredSlots[i].GetComponent<RectTransform>();
                if (itemRect == null || !itemMergeRequiredSlots[i].gameObject.activeSelf) continue;

                float maxDistance = Vector3.Distance(itemRect.position, targetPosition);
                bool isMoving = MoveAndScaleItem(itemRect, targetPosition, initialScale, targetScale, maxDistance, minDistance);

               
                if (isMoving)
                    allItemsReached = false;
            }

            yield return null;
        }

        
        this.ResetItemScales();
    }

    private void SetActiveMergeRequiredSlots(int itemCount)
    {
        for (int i = 0; i < itemMergeRequiredSlots.Count; i++)
        {
            itemMergeRequiredSlots[i].gameObject.SetActive(i < itemCount);
        }
    }

    private bool MoveAndScaleItem(RectTransform itemRect, Vector3 targetPosition, Vector3 initialScale, Vector3 targetScale, float maxDistance, float minDistance)
    {
        float distance = Vector3.Distance(itemRect.position, targetPosition);
        if (distance > minDistance)
        {
            itemRect.position = Vector3.Lerp(itemRect.position, targetPosition, moveSpeed * Time.deltaTime);
            float scaleLerp = Mathf.Clamp01(1 - (distance / maxDistance));
            itemRect.localScale = Vector3.Lerp(initialScale, targetScale, scaleLerp);
            return true; // Continue moving
        }
        else
        {
            itemRect.position = targetPosition;
            itemRect.localScale = initialScale;
            return false; // Stop moving
        }
    }

    private void ResetItemScales()
    {
        foreach (var item in itemMergeRequiredSlots)
        {
            if (item != null && item.gameObject.activeSelf)
            {
                item.transform.localScale = Vector3.one;
                item.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator MergeFinal(List<ShipItemEquipData> shipItemEquipDatas)
    {
        yield return new WaitForSeconds(0.1f);
        AudioManager.Instance.SpawnSFX(SoundCode.MergeFly);
        if (shipItemEquipDatas.Count > 0)
        {
            itemMergedSlot.gameObject.SetActive(false);
            itemMergedSlot.SetItemMergedUI(shipItemEquipDatas[0]);
            itemMergedSlot.gameObject.SetActive(true);

        }
    }

    private IEnumerator ActivateButtonAndCloseText()
    {
        yield return new WaitForSeconds(0.1f);
        button.interactable = true;
        closeText.gameObject.SetActive(true);
    }

    private void DisableButtonAndCloseText()
    {
        button.interactable = false;
        closeText.gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadIItemMerge();
        LoadRequiredItemsSlots();
        LoadItemMergedDetailStat();
        LoadCloseText();
    }

    protected virtual void LoadIItemMerge()
    {
        if (itemMergedSlot != null) return;
        itemMergedSlot = transform.Find("ItemMergeSlot").GetComponentInChildren<ItemMergedSlot>(true);
        Debug.Log($"{transform.name} LoadIItemMerge", gameObject);
    }

    protected virtual void LoadItemMergedDetailStat()
    {
        if (itemMergedDetailStat != null) return;
        itemMergedDetailStat = transform.GetComponentInChildren<ItemMergedDetailStat>(true);
        Debug.Log($"{transform.name} Load itemMergedDetailStat", gameObject);
    }

    protected virtual void LoadCloseText()
    {
        if (closeText != null) return;
        closeText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        Debug.Log($"{transform.name} Load CloseText", gameObject);
    }

    protected virtual void LoadRequiredItemsSlots()
    {
        if (itemMergeRequiredSlots.Count > 0) return;

        foreach (Transform obj in transform.Find("ItemRequiredSlot"))
        {
            ItemMergeRequiredSlot requiredSlot = obj.GetComponentInChildren<ItemMergeRequiredSlot>(true);
            if (requiredSlot != null)
            {
                itemMergeRequiredSlots.Add(requiredSlot);
                requiredSlot.gameObject.SetActive(false);
            }
        }
        Debug.Log($"{transform.name} Loaded Required Items: {itemMergeRequiredSlots.Count}", gameObject);
    }

    protected override void OnClick()
    {
        ShipItemMergeManager.Instance.MergeItem();
    }
}

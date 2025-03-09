using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiSelectableSlot : ButtonBase
{
    [SerializeField] protected TextMeshProUGUI UiItemName;
    [SerializeField] protected Image UiItemImage;
    [SerializeField] protected TextMeshProUGUI UIItemStats;
    [SerializeField] protected UiSelectableLevel uiSelectableLevel;
    [SerializeField] protected Image UIItemEvo;
    [SerializeField] protected TextMeshProUGUI Description;

    [SerializeField] protected Selectable selectable;
    public event Action<Selectable> OnItemSelected;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUiItemName();
        this.LoadUiDescription();
        this.LoadItemLevel();
        this.LoadUiItemImage();
        this.LoadUIItemStats();
        this.LoadUIItemEvo();
    }

    protected virtual void LoadUiItemName()
    {
        if (this.UiItemName != null) return;
        this.UiItemName = transform.Find("Selectable_Name")?.GetComponentInChildren<TextMeshProUGUI>();
        if (this.UiItemName == null) Debug.LogWarning("UiItemName not found in " + transform.name);
    }
    protected virtual void LoadUiDescription()
    {
        if (this.Description != null) return;
        this.Description = transform.Find("Selectable_Description")?.GetComponentInChildren<TextMeshProUGUI>();
        if (this.Description == null) Debug.LogWarning("UiItemName not found in " + transform.name);
    }

    protected virtual void LoadItemLevel()
    {
        if (this.uiSelectableLevel != null) return;
        this.uiSelectableLevel = transform.GetComponentInChildren<UiSelectableLevel>();
        if (this.uiSelectableLevel == null) Debug.LogWarning("UiSelectableLevel not found in " + transform.name);
    }

    protected virtual void LoadUiItemImage()
    {
        if (this.UiItemImage != null) return;
        this.UiItemImage = transform.Find("Selectable_Image")?.GetComponentInChildren<Image>();
        if (this.UiItemImage == null) Debug.LogWarning("UiItemImage not found in " + transform.name);
    }

    protected virtual void LoadUIItemStats()
    {
        if (this.UIItemStats != null) return;
        this.UIItemStats = transform.Find("Selectable_Description/BG")?.GetComponentInChildren<TextMeshProUGUI>();
        if (this.UIItemStats == null) Debug.LogWarning("UIItemStats not found in " + transform.name);
    }

    protected virtual void LoadUIItemEvo()
    {
        if (this.UIItemEvo != null) return;
        this.UIItemEvo = transform.Find("Selectable_Evo")?.GetComponentInChildren<Image>();
        if (this.UIItemEvo == null) Debug.LogWarning("UIItemEvo not found in " + transform.name);
    }

    public virtual void SetUI(Selectable selectable, int levels, bool isEvoLevel)
    {
        this.ResetUI();
        this.selectable = selectable;

        if (UiItemName != null) UiItemName.text = selectable.itemName;
        if (UiItemImage != null) UiItemImage.sprite = selectable.itemSprite;

        switch (selectable.selectableType)
        {
            case SelectableType.Ability:
                AbilityData abilityData = selectable as AbilityData;
                if (UIItemEvo != null)
                {
                    AbilityLevelData levelData = abilityData.GetLevelData(levels + 1);
                    Description.text = levelData?.GetDescription() ?? "No description available.";
                    UIItemEvo.gameObject.SetActive(true);
                    UIItemEvo.sprite = abilityData.abilityBuffData.itemSprite;
                }
                if (isEvoLevel)
                {
                    Color advancedColor = new Color32(255, 0, 255, 255);
                    if (UiItemName != null) UiItemName.color = advancedColor;
                    if (button != null) button.image.color = advancedColor;
                }
                else
                {
                    if (UiItemName != null) UiItemName.color = Color.white;
                    if (button != null) button.image.color = Color.white;
                }
                break;

            case SelectableType.AbilityBuff:
                AbilityBuffData abilityBuffData = selectable as AbilityBuffData;
                AbilityBuffLevelData abilityBuffLevelData = abilityBuffData.GetAbilityLevelData(levels + 1);
                Description.text = abilityBuffLevelData?.GetDescription() ?? "No description available.";
                if (UIItemEvo != null) UIItemEvo.sprite = abilityBuffData.abilityData.itemSprite;
                if (UiItemName != null) UiItemName.color = Color.white;
                if (button != null) button.image.color = Color.white;
                break;
            case SelectableType.AbilityBuffShip:
                AbilityBuffShipData abilityBuffShipData = selectable as AbilityBuffShipData;
                AbilityBuffShipLevelData abilityBuffShipLevelData = abilityBuffShipData.GetAbilityBuffShipLevel(levels + 1);
                Description.text = abilityBuffShipLevelData?.GetDescription() ?? "No description available.";
                if (UiItemName != null) UiItemName.color = Color.white;
                if (button != null) button.image.color = Color.white;
                if (UIItemEvo != null) UIItemEvo.gameObject.SetActive(false);
                break;

            case SelectableType.Item:
                ItemData itemData = selectable as ItemData;
                Description.text = itemData?.GetDescription() ?? "No description available.";
                if (UiItemName != null) UiItemName.color = Color.white;
                if (button != null) button.image.color = Color.white;
                if (UIItemEvo != null) UIItemEvo.gameObject.SetActive(false);
                break;
        }

        if (uiSelectableLevel != null)
            uiSelectableLevel.SetLevel(levels, isEvoLevel && selectable is AbilityData);
    }

    public virtual void ResetUI()
    {
        if (UiItemName != null) UiItemName.text = string.Empty;
        if (uiSelectableLevel != null) uiSelectableLevel.SetLevel(0, false);
        if (UiItemImage != null) UiItemImage.sprite = null;
        if (UIItemEvo != null)
        {
            UIItemEvo.gameObject.SetActive(true);
            UIItemEvo.sprite = null;
        }
        this.selectable = null;
    }

    protected override void OnClick()
    {
        this.OnItemSelected?.Invoke(this.selectable);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMergedDetailStat : FuncManager
{
    [SerializeField] protected List<ItemMergedTextUI> itemMergedTextUIs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRequiredItemsSlots();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.ResetSlotUI();
    }

    public IEnumerator ShowUIWithDelay(ShipItemEquipData currentTier)
    {
        if (currentTier == null) yield break;
        currentTier.CalculateBoostedAttributes();
        ShipItemEquipData nextTier = new()
        {
            shipItemData = currentTier.shipItemData,
            currentTier = currentTier.currentTier + 1,
            currentLevel = currentTier.currentLevel,
        };
        nextTier.CalculateBoostedAttributes();

        foreach (var item in this.itemMergedTextUIs)
        {
            yield return new WaitForSeconds(0.5f);
            item.gameObject.SetActive(true);
            switch (item.name)
            {
                case "Item_LvMax":
                    item.ShowMergedItem(currentTier.GetMaxLevel().ToString(), nextTier.GetMaxLevel().ToString());
                    break;
                case "Item_HP":
                    item.ShowMergedItem(currentTier.currentHP.ToString(), nextTier.currentHP.ToString());
                    break;
                case "Item_ATK":
                    item.ShowMergedItem(currentTier.currentATK.ToString(), nextTier.currentATK.ToString());
                    break;
                case "Item_Boosted":
                    item.ShowMergedItem(
                        $"{currentTier.GetShipItemTierData().itemtierBoostAmount}%",
                        $"{nextTier.GetShipItemTierData().itemtierBoostAmount}%"
                    );
                    break;
            }
        }
    }

    protected virtual void LoadRequiredItemsSlots()
    {
        if (itemMergedTextUIs.Count > 0) return;

        foreach (Transform obj in transform)
        {
            ItemMergedTextUI requiredSlot = obj.GetComponentInChildren<ItemMergedTextUI>(true);
            if (requiredSlot != null)
            {
                this.itemMergedTextUIs.Add(requiredSlot);
                requiredSlot.gameObject.SetActive(false);
            }
        }
        Debug.Log($"{transform.name} Loaded Required Items: {this.itemMergedTextUIs.Count}", gameObject);
    }

    private void ResetSlotUI()
    {
        foreach (var item in itemMergedTextUIs)
        {
            item.gameObject.SetActive(false);
        }
    }
}

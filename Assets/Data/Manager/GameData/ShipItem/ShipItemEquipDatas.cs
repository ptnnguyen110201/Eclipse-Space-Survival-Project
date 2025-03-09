using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ShipItemEquipDatas
{
    public int baseHP;
    public int baseATK;

    public int boostedHP;
    public int boostedATK;

    public int currentATK;
    public int currentHP;


    public List<ShipItemEquipData> ShipItemEquiping = new List<ShipItemEquipData>();
    public List<ShipItemEquipData> ShipItemIventory = new List<ShipItemEquipData>();
    public bool hasEquipItem() => this.ShipItemEquiping.Count > 0;
    public bool MergeItem(List<ShipItemEquipData> mergeRequireItems, PlayerData playerData)
    {
        if (mergeRequireItems == null || mergeRequireItems.Count == 0)
        {
            Debug.LogError("Merge failed: mergeRequireItems is null or empty.");
            return false;
        }

        List<ShipItemEquipData> allItems = new List<ShipItemEquipData>(this.ShipItemEquiping);
        allItems.AddRange(this.ShipItemIventory);

        ShipItemEquipData itemToUpgrade = mergeRequireItems.OrderByDescending(item => item.currentLevel).FirstOrDefault();
        if (itemToUpgrade == null)
        {
            Debug.LogError("Merge failed: No valid item to upgrade.");
            return false;
        }

        ShipTierRecipeIngredient shipTierRecipe = itemToUpgrade.GetShipItemTierData()?.ingredients;
        if (shipTierRecipe == null)
        {
            Debug.LogError("Merge failed: shipTierRecipe is null.");
            return false;
        }

        if (shipTierRecipe.Amount > mergeRequireItems.Count)
        {
            Debug.LogWarning($"Merge failed: Not enough items to merge. Required: {shipTierRecipe.Amount}, Provided: {mergeRequireItems.Count}");
            return false;
        }
        foreach (var item in mergeRequireItems)
        {
            if (!allItems.Contains(item))
            {
                Debug.LogWarning($"Merge failed: Item {item.shipItemData?.itemName} not found in inventory or equipped list.");
                return false;
            }
        }

        mergeRequireItems.Remove(itemToUpgrade);
        foreach (var item in mergeRequireItems)
        {
            int indexToRemove = allItems.IndexOf(item);
            if (indexToRemove >= 0)
            {
                allItems.RemoveAt(indexToRemove);
            }
        }
        if (itemToUpgrade.shipItemData?.shipItemTierDatas != null) 
        {
            itemToUpgrade.currentTier = Mathf.Min(itemToUpgrade.currentTier + 1, itemToUpgrade.shipItemData.shipItemTierDatas.Count);
            Debug.Log($"Item {itemToUpgrade.shipItemData.itemName} upgraded to tier {itemToUpgrade.currentTier}.");
        }
        else
        {
            Debug.LogError($"Merge failed: shipItemTierDatas is null or empty for item {itemToUpgrade.shipItemData?.itemName}.");
            return false;
        }

        this.ShipItemEquiping = allItems.Where(item => item.isEquipped).ToList();
        this.ShipItemIventory = allItems.Where(item => !item.isEquipped).ToList();

        itemToUpgrade.CalculateBoostedAttributes();
        this.UpdateAttributes();
        return true;
    }



    public bool UpLevelItem(PlayerData playerData, ShipItemEquipData itemtoUplevel, int level)
    {
        if (itemtoUplevel == null || level <= 0) return false;
        if (itemtoUplevel.currentLevel >= itemtoUplevel.GetMaxLevel()) return false;
        ShipItemLevelData shipItemLevelData = itemtoUplevel.GetShipItemLevelData();
        bool enoughtCurrency = playerData.currencyData.SpendCurrency(playerData, shipItemLevelData.Ingredients.Amount, shipItemLevelData.Ingredients.Currency);
        if (!enoughtCurrency) return false;
        itemtoUplevel.currentLevel += level;
        itemtoUplevel.CalculateBoostedAttributes();
        this.UpdateAttributes();
        playerData.questDatas.UpdateQuestProgress(QuestType.ItemLevelUp, 1);
        return true;
    }
    public void AddItem(ShopEquipItemData ShopEquipItemData)
    {
        if (ShopEquipItemData == null) return;
        ShipItemEquipData shipItemEquipData = new()
        {
            shipItemData = ShopEquipItemData.shipItemData,
            isEquipped = false,
            currentLevel = ShopEquipItemData.currentLevel,
            currentTier = ShopEquipItemData.currentTier,
        };
        shipItemEquipData.CalculateBoostedAttributes();
        this.ShipItemIventory.Add(shipItemEquipData);

    }
    public void AddItem(ShipItemData shipItemData) 
    {
        ShipItemEquipData shipItemEquipData = new()
        {
            shipItemData = shipItemData,
            isEquipped = false,
            currentTier = shipItemData.itemTier,
            currentLevel = shipItemData.itemLevel,
        };
        shipItemEquipData.CalculateBoostedAttributes();
        this.ShipItemIventory.Add(shipItemEquipData);
        
    }
    public void EquipItem(ShipItemEquipData itemToEquip)
    {
        if (itemToEquip.isEquipped || !this.ShipItemIventory.Contains(itemToEquip)) return;

        ShipItemEquipData existingItem = this.ShipItemEquiping.Find(item => item.shipItemData.shipItemType == itemToEquip.shipItemData.shipItemType);

        if (existingItem == null)
        {
            int indexToRemove = this.ShipItemIventory.IndexOf(itemToEquip);
            if (indexToRemove >= 0)
            {
                this.ShipItemIventory.RemoveAt(indexToRemove);
                this.ShipItemEquiping.Add(itemToEquip);
                itemToEquip.isEquipped = true;
            }
        }
        else
        {
            this.SwapItems(itemToEquip, existingItem);
        }
        itemToEquip.CalculateBoostedAttributes();
        this.UpdateAttributes();

    }
    public void UnequipItem(ShipItemEquipData itemToUnequip)
    {
        if (!itemToUnequip.isEquipped || !this.ShipItemEquiping.Contains(itemToUnequip)) return;
        this.ShipItemEquiping.Remove(itemToUnequip);
        this.ShipItemIventory.Add(itemToUnequip);
        itemToUnequip.isEquipped = false;
        itemToUnequip.CalculateBoostedAttributes();
        this.UpdateAttributes();

    }
    private void SwapItems(ShipItemEquipData newItem, ShipItemEquipData existingItem)
    {

        if (newItem == null || existingItem == null) return;
        if (!this.ShipItemIventory.Contains(newItem) || !this.ShipItemEquiping.Contains(existingItem)) return;

        int newItemIndex = this.ShipItemIventory.IndexOf(newItem);
        int existingItemIndex = this.ShipItemEquiping.IndexOf(existingItem);

        newItem.isEquipped = true;
        existingItem.isEquipped = false;

        this.ShipItemIventory[newItemIndex] = existingItem;
        this.ShipItemEquiping[existingItemIndex] = newItem;
        newItem.CalculateBoostedAttributes();
        existingItem.CalculateBoostedAttributes();
        this.UpdateAttributes();
    }

    private void UpdateAttributes()
    {
        this.baseHP = 0;
        this.baseATK = 0;

        ShipItemEquipData shipHull = null;

        foreach (var itemEquipData in ShipItemEquiping)
        {
            if (itemEquipData == null || !itemEquipData.isEquipped || itemEquipData.shipItemData == null) continue;

            if (itemEquipData.shipItemData.shipItemType == ShipItemType.ShipHull)
            {
                shipHull = itemEquipData;
            }
            itemEquipData.CalculateBoostedAttributes();
            this.baseHP += itemEquipData.currentHP;
            this.baseATK += itemEquipData.currentATK;
        }

        this.currentHP = this.baseHP;
        this.currentATK = this.baseATK;
        if (shipHull == null)
        {
            this.boostedHP = 0;
            this.boostedATK = 0;
        }
        else
        {
            int IterBoosted = shipHull.GetShipItemTierData().itemtierBoostAmount;
            foreach (ShipItemAttributes attribute in shipHull.GetShipItemLevelData().ShipItemAttributes)
            {
                switch (attribute.Attribute)
                {
                    case ShipItemAttributesCode.HP:
                        this.boostedHP = (int)((this.baseHP * IterBoosted) / 100.0f);
                        break;
                    case ShipItemAttributesCode.ATK:
                        this.boostedATK = (int)((this.baseATK * IterBoosted) / 100.0f);
                        break;
                }
            }

        }
        this.currentHP += this.boostedHP;
        this.currentATK += this.boostedATK;
    }
}

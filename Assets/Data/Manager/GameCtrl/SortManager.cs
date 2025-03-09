using System.Collections.Generic;
using UnityEngine;

public static class SortManager
{
    public static List<SortType> SortTypes = new List<SortType>
    {
        SortType.ItemType,
        SortType.ItemTier,
        SortType.ItemLevel
    };

    public static List<ShipItemEquipData> SortItems(List<ShipItemEquipData> shipItems, SortType sortType)
    {
        List<ShipItemEquipData> sortedItems = new List<ShipItemEquipData>(shipItems);
        sortedItems.RemoveAll(item => item == null);
        switch (sortType)
        {
            case SortType.ItemType:
                sortedItems.Sort((x, y) =>
                {
                    int typeComparison = x.shipItemData.shipItemType.CompareTo(y.shipItemData.shipItemType);
                    if (typeComparison != 0) return typeComparison;
                    int idComparison = y.shipItemData.ItemID.CompareTo(x.shipItemData.ItemID);
                    if (idComparison != 0) return idComparison;
                    int tierComparison = y.currentTier.CompareTo(x.currentTier);
                    if (tierComparison != 0) return tierComparison;
                    return y.currentLevel.CompareTo(x.currentLevel);
                });
                break;

            case SortType.ItemLevel:
                sortedItems.Sort((x, y) =>
                {
                    int levelComparison = y.currentLevel.CompareTo(x.currentLevel);
                    if (levelComparison != 0) return levelComparison;
                    int typeComparison = x.shipItemData.shipItemType.CompareTo(y.shipItemData.shipItemType);
                    if (typeComparison != 0) return typeComparison;
                    int idComparison = y.shipItemData.ItemID.CompareTo(x.shipItemData.ItemID);
                    if (idComparison != 0) return idComparison;
                    return y.currentTier.CompareTo(x.currentTier);
                });
                break;

            case SortType.ItemTier:
                sortedItems.Sort((x, y) =>
                {
                    int tierComparison = y.currentTier.CompareTo(x.currentTier);
                    if (tierComparison != 0) return tierComparison;

                    int typeComparison = x.shipItemData.shipItemType.CompareTo(y.shipItemData.shipItemType);
                    if (typeComparison != 0) return typeComparison;

                    int idComparison = y.shipItemData.ItemID.CompareTo(x.shipItemData.ItemID);
                    if (idComparison != 0) return idComparison;
                    return y.currentLevel.CompareTo(x.currentLevel);
                });
                break;
        }

        return sortedItems;
    }


}

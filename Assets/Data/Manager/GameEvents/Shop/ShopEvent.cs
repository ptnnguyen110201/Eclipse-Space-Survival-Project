using System;

public static class ShopEvent
{
    public static event Action OnShopRefreshed;
    public static event Action<ShipItemData> OnItemPurchased;
    public static void TriggerShopRefreshed()
    {
        OnShopRefreshed?.Invoke();
    }

    public static void TriggerItemPurchased(ShipItemData item)
    {
        OnItemPurchased?.Invoke(item);
    }
}

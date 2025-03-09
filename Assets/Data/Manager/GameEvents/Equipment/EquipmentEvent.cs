using System;

public static class EquipmentEvent
{

    public static event Action<ShipItemData> OnItemUnequipped;
    public static event Action<ShipItemData> OnItemEquipped;

    public static void TriggerItemUnequipped(ShipItemData item)
    {
        OnItemUnequipped?.Invoke(item);
    }
    public static void TriggerItemEquipped(ShipItemData item)
    {
        OnItemEquipped?.Invoke(item);
    }

}

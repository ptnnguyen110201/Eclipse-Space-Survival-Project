using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ItemData", menuName = "Object/ItemData")]
public class ItemData : Selectable
{
    public ItemType itemType;
    public int Amount;

    public string GetDescription()
    {
        switch (itemType)
        {
            case ItemType.Health:
                return $"Restores {Amount}% of HP.";
            case ItemType.Gold:
                return $"Grants {Amount} gold.";
            default:
                return "Unknown item.";
        }
    }
}

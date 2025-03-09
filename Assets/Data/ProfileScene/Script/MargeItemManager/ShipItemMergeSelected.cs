using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipItemMergeSelected
{
    public List<ShipItemEquipData> shipItemEquipDatas = new List<ShipItemEquipData>();

    public bool AreItemsAtMax()
    {
        if (this.shipItemEquipDatas.Count == 0) return false;
        ShipItemEquipData item = this.shipItemEquipDatas[0];
        if (item == null) return false;

        int maxItems = item.GetShipItemTierData().ingredients.Amount;
        return this.shipItemEquipDatas.Count >= maxItems + 1;
    }
    public virtual void ToggleSelectShipItemEquip(ShipItemEquipData shipItemEquipData)
    {
        if (shipItemEquipData == null) return;

        if (this.shipItemEquipDatas.Contains(shipItemEquipData))
        {
            this.RemoveShipItemEquip(shipItemEquipData);
            return;
        }

        if (!this.AreItemsAtMax())
        {
            this.shipItemEquipDatas.Add(shipItemEquipData);
            return;
        }
    }

    public void RemoveShipItemEquip(ShipItemEquipData shipItemEquipDataToRemove)
    {
        if (shipItemEquipDataToRemove == null) return;
        int index = this.shipItemEquipDatas.IndexOf(shipItemEquipDataToRemove);
        if (index < 0) return;
        if (index == 0)
        {
            this.shipItemEquipDatas.Clear();
            return;
        }
        this.shipItemEquipDatas.RemoveAt(index);


    }

    public void ClearSelections()
    {
        this.shipItemEquipDatas.Clear();
        Debug.Log("Cleared all ship item selections.");
    }

}

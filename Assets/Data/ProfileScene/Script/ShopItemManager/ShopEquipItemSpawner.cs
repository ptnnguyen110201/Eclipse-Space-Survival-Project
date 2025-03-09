using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEquipItemSpawner : Spawner
{
    protected override void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("EquipItem");
        Debug.Log(transform.name + "Load Holder", gameObject);
    }

    public void SpawnItem(ShopEquipItemDatas ShopEquipItemDatas)
    {
        this.ClearItem();
        int itemCount = ShopEquipItemDatas.currentShopItems.Count;
        for (int i = 0; i < itemCount; i++)
        {
            Transform newItem = this.Spawn("ShopEquipItemSlot", transform.position, transform.rotation);
            newItem.localScale = Vector3.one;
            ShopEquipItemData shopEquiItemData = ShopEquipItemDatas.currentShopItems[i];
            ShopEquipItemSlot slot = newItem.GetComponent<ShopEquipItemSlot>();
            slot.SetItem(shopEquiItemData, shopEquiItemData.IsPurchased);
            UIManager.Instance.OpenScaleUp(newItem.gameObject, 0.5f);
        }
    }
    private void ClearItem() 
    {
        foreach (Transform item in this.holder) 
        {
            this.Despawn(item);
        }
    }
}

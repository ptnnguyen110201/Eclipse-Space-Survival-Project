using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipItemMergedSlot : ButtonBase
{
    Color32[] colors = new Color32[]
{
    new Color(0, 1, 0, 1f),
    new Color(0, 1, 1, 1f),
    new Color(0, 0, 1 ,1f),
    new Color(1, 0, 0 ,1f),
    new Color(1, 0, 1, 1f),
}; [SerializeField] protected Image Item_Slot;
    [SerializeField] protected Image Item_Image;
    [SerializeField] protected TextMeshProUGUI Item_Level;
    [SerializeField] protected ShipItemEquipData shipItemEquipData;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItem_Slot();
        this.LoadItem_Image();
        this.LoadItem_Level();
    }


    public virtual void SetItemUI(ShipItemEquipData shipItemEquipData)
    {
        if (shipItemEquipData == null) return;
        ShipItemEquipData itemEquipData = new ShipItemEquipData
        {
            shipItemData = shipItemEquipData.shipItemData,
            currentLevel = shipItemEquipData.currentLevel,
            currentTier = shipItemEquipData.currentTier + 1,
        };
        
        this.shipItemEquipData = itemEquipData;
        this.Item_Level.text = $"Lv.{itemEquipData.GetShipItemLevelData().itemLevel}";
        this.StartCoroutine(FlashColor(itemEquipData));
    }
    private IEnumerator FlashColor(ShipItemEquipData shipItemEquipData)
    {
        this.Item_Level.text = $"Lv.{shipItemEquipData.GetShipItemLevelData().itemLevel}";

        this.Item_Slot.color = colors[shipItemEquipData.currentTier - 1];
        this.Item_Image.sprite = shipItemEquipData.GetShipItemTierData().itemTierSprite;
        Color32 originalSlotColor = Item_Slot.color;
        Color32 originalColor = new Color(1, 1, 1, 1);
        Color32 flashColor = new Color(0f, 0f, 0f, 0f);
        float interval = 1;

        while (true)
        {
            for (float t = 0; t < 1; t += Time.deltaTime / interval)
            {
                this.Item_Slot.color = Color32.Lerp(originalSlotColor, flashColor, t);
                this.Item_Image.color = Color32.Lerp(originalColor, flashColor, t);
                this.Item_Level.color = Color32.Lerp(originalColor, flashColor, t);
                yield return null;
            }

            for (float t = 0; t < 1; t += Time.deltaTime / interval)
            {
                this.Item_Slot.color = Color32.Lerp(flashColor, originalSlotColor, t);
                this.Item_Image.color = Color32.Lerp(flashColor, originalColor, t);
                this.Item_Level.color = Color32.Lerp(flashColor, originalColor, t);
                yield return null;
            }
        }
    }
    protected virtual void LoadItem_Slot()
    {
        if (this.Item_Slot != null) return;
        this.Item_Slot = transform.GetComponent<Image>();
        Debug.Log(transform.name + "LoadItem_Slot ", gameObject);
    }
    protected virtual void LoadItem_Image()
    {
        if (this.Item_Image != null) return;
        this.Item_Image = transform.Find("Item_Image").GetComponent<Image>();
        Debug.Log(transform.name + "Load Item_Image ", gameObject);
    }
    protected virtual void LoadItem_Level()
    {
        if (this.Item_Level != null) return;
        this.Item_Level = transform.Find("Item_Level").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + "Load Item_Level ", gameObject);
    }
    protected override void OnClick()
    {

    }

}


using UnityEngine;
[CreateAssetMenu(fileName = "ShipData", menuName = "ShipData/ShipData")]
public class ShipData : ScriptableObject
{
    public Sprite sprite;
    public ShipLevelMap shipLevelMap;
    private void OnEnable()
    {
        this.LoadLevelDatabase();
    }
    public virtual void LoadLevelDatabase()
    {
        if (this.shipLevelMap != null) return;
        string resPath = "ShipDatas/ShipLevelMap";
        this.shipLevelMap = Resources.Load<ShipLevelMap>(resPath);
        Debug.LogWarning(": LoadLevelDatabase " + resPath);
    }
    public void SetImage(PlayerData playerData)
    {
        if (playerData == null) return;
        foreach (ShipItemEquipData equipData in playerData.shipItemEquipDatas.ShipItemEquiping)
        {
            if (equipData.shipItemData == null) continue;

            ShipItemType itemType = equipData.shipItemData.shipItemType;
            ShipItemTierData tierData = equipData.GetShipItemTierData();

            if (itemType != ShipItemType.ShipHull) continue;
            this.sprite = tierData.itemTierSprite;
        }
    }
}

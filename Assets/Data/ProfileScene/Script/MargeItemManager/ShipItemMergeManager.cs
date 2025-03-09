using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipItemMergeManager : FuncManager
{
    private static ShipItemMergeManager instance;
    public static ShipItemMergeManager Instance => instance;
    [Header("Data")]
    private PlayerData playerData;

    [Header("Spawner")]
    [SerializeField] protected ShipItemMergeSelected shipItemMergeSelected;
    [SerializeField] protected ShipItemMergeSpawner ShipItemMergeSpawner;
    [SerializeField] protected ShipItemMergingSpawner shipItemMergingSpawner;
    [SerializeField] protected ItemMergedStat shipItemMergeStat;
    [SerializeField] protected SortType currentSortType = SortType.ItemType;
    [SerializeField] protected ShipItemMergeBtn MergeBtn;


    protected override void Awake()
    {
        if (ShipItemMergeManager.instance != null) Debug.LogError("Only 1 ShipItemMergeManager allowed to exist");
        ShipItemMergeManager.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipItemMergeSpawner();
        this.LoadShipItemMergingSpawner();
        this.LoadShipMergeBtn();
        this.LoadItemMergedStat();
    }
    protected virtual void LoadShipItemMergeSpawner()
    {
        if (this.ShipItemMergeSpawner != null) return;
        this.ShipItemMergeSpawner = transform.GetComponentInChildren<ShipItemMergeSpawner>();
        Debug.Log(transform.name + "Load ShipItemMergeSpawner", gameObject);
    }
    protected virtual void LoadShipItemMergingSpawner()
    {
        if (this.shipItemMergingSpawner != null) return;
        this.shipItemMergingSpawner = transform.GetComponentInChildren<ShipItemMergingSpawner>();
        Debug.Log(transform.name + "Load ShipItemMergingSpawner", gameObject);
    }
    protected virtual void LoadShipMergeBtn()
    {
        if (this.MergeBtn != null) return;
        this.MergeBtn = transform.Find("ItemMergeBtnBar").GetComponentInChildren<ShipItemMergeBtn>(true);
        this.MergeBtn.gameObject.SetActive(false);
        Debug.Log(transform.name + "Load ShipMergeBtn", gameObject);
    }
    protected virtual void LoadItemMergedStat()
    {
        if (this.shipItemMergeStat != null) return;
        this.shipItemMergeStat = transform.Find("ItemMergedStat").GetComponentInChildren<ItemMergedStat>(true);
        this.shipItemMergeStat.gameObject.SetActive(false);
        Debug.Log(transform.name + "Load ItemMergedStat", gameObject);
    }
    private void ResetUI()
    {
        this.SpawnItemInventory();
    }

    private void SpawnItemInventory()
    {
        List<ShipItemEquipData> Inventory = this.playerData.shipItemEquipDatas.ShipItemIventory;
        List<ShipItemEquipData> EquipingItem = this.playerData.shipItemEquipDatas.ShipItemEquiping;

        List<ShipItemEquipData> allItems = new List<ShipItemEquipData>(Inventory);
        allItems.AddRange(EquipingItem);
        this.ShipItemMergeSpawner.SpawnItem(this.shipItemMergeSelected.shipItemEquipDatas, allItems, this.currentSortType);
    }
  
    public void MergeItem()
    {
        List<ShipItemEquipData> selectedItem = this.shipItemMergeSelected.shipItemEquipDatas;
        if (selectedItem.Count <= 0) return;
        this.playerData.shipItemEquipDatas.MergeItem(selectedItem,this.playerData);
        this.shipItemMergeSelected.ClearSelections();
        this.shipItemMergingSpawner.ShowMergeUI(this.shipItemMergeSelected.shipItemEquipDatas);
        this.shipItemMergeStat.gameObject.SetActive(false);
        this.MergeBtn.gameObject.SetActive(false);
        this.ResetUI();
    }
    public void OpenItemMergedStat()
    {
        List<ShipItemEquipData> selectedItem = this.shipItemMergeSelected.shipItemEquipDatas;    
        this.shipItemMergeStat.gameObject.SetActive(true);
        this.shipItemMergeStat.ShowUI(selectedItem);

    }
    public void SelectItemEquipData(ShipItemEquipData selectedItem)
    {
        if (selectedItem == null) return;
        this.shipItemMergeSelected.ToggleSelectShipItemEquip(selectedItem);
        bool isMax = this.shipItemMergeSelected.AreItemsAtMax();
        this.shipItemMergingSpawner.ShowMergeUI(this.shipItemMergeSelected.shipItemEquipDatas);
        this.MergeBtn.gameObject.SetActive(isMax);
        this.ResetUI();
    }

    public void OpenMergeBar()
    {
        UIManager.Instance.OpenElement(this.gameObject);
        this.GetPlayerAndGameData();
        this.shipItemMergeSelected.ClearSelections();
        this.ResetUI();
    }
    private void GetPlayerAndGameData()
    {
        this.playerData = PlayerDataLoad.Instance.GetPlayerData();
    }
}

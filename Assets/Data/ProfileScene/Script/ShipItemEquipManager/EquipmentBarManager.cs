using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentBarManager : FuncManager
{
    private static EquipmentBarManager instance;
    public static EquipmentBarManager Instance => instance;
    [Header("Spawner")]
    [SerializeField] protected ShipUIDetail shipUIDetail;
    [SerializeField] protected ShipItemEquipSpawner shipItemEquipSpawner;
    [SerializeField] protected ShipEquipingUI shipEquipingUI;
    [SerializeField] protected SortType currentSortType = SortType.ItemType;

    [Header("Stat")]
    [SerializeField] protected Transform ItemEquipStat;


    protected override void Awake()
    {
        if (EquipmentBarManager.instance != null) Debug.LogError("Only 1 EquipmentBarManager allowed to exist");
        EquipmentBarManager.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipUIDetail();
        this.LoadShipItemEquipSpawner();
        this.LoadShipEquipingUI();
        this.LoadItemEquipStat();

    }
    protected virtual void LoadShipUIDetail()
    {
        if (this.shipUIDetail != null) return;
        this.shipUIDetail = transform.GetComponentInChildren<ShipUIDetail>();
        Debug.Log(transform.name + "Load ShipUIDetail", gameObject);
    }
    protected virtual void LoadShipItemEquipSpawner()
    {
        if (this.shipItemEquipSpawner != null) return;
        this.shipItemEquipSpawner = transform.GetComponentInChildren<ShipItemEquipSpawner>();
        Debug.Log(transform.name + "Load ShipItemEquipSpawner", gameObject);
    }
    protected virtual void LoadShipEquipingUI()
    {
        if (this.shipEquipingUI != null) return;
        this.shipEquipingUI = transform.GetComponentInChildren<ShipEquipingUI>();
        Debug.Log(transform.name + "Load ShipEquipingUI", gameObject);
    }
    protected virtual void LoadItemEquipStat()
    {
        if (this.ItemEquipStat != null) return;
        this.ItemEquipStat = transform.Find("ItemEquipStat").GetComponent<Transform>();
        this.ItemEquipStat.gameObject.SetActive(false);
        Debug.Log(transform.name + "Load ItemEquipStat", gameObject);
    }
    protected virtual void ResetUI()
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;

        this.shipUIDetail.SetShipDetailUI(playerData.shipItemEquipDatas);
        List<ShipItemEquipData> inventory = playerData.shipItemEquipDatas.ShipItemIventory;
        List<ShipItemEquipData> equipingItem = playerData.shipItemEquipDatas.ShipItemEquiping;

        this.shipItemEquipSpawner.SpawnItem(inventory, this.currentSortType);
        this.shipEquipingUI.SpawnItemEquiping(equipingItem);

        PlayerDataLoad.Instance.SaveData();
    }
    public SortType ChangeSortType()
    {
        int currentIndex = SortManager.SortTypes.IndexOf(this.currentSortType);
        currentIndex = (currentIndex + 1) % SortManager.SortTypes.Count;
        this.currentSortType = SortManager.SortTypes[currentIndex];
        return this.currentSortType;
    }
    public void EquipItem(ShipItemEquipData itemToEquip)
    {
        if (itemToEquip == null) return;
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        playerData. shipItemEquipDatas.EquipItem(itemToEquip);
        this.ResetUI();

    }

    public void UnequipItem(ShipItemEquipData itemToUnequip)
    {
       
        if (itemToUnequip == null) return;
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        playerData.shipItemEquipDatas.UnequipItem(itemToUnequip);
        this.ResetUI();
    }
    public void UpLevelItem(ShipItemEquipData itemToUplevel, int level) 
    {
        if (itemToUplevel == null) return;
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        bool CanUplevel = playerData.shipItemEquipDatas.UpLevelItem(playerData ,itemToUplevel, level);
        if (!CanUplevel) return;
        ShipItemEquipStat shipItemEquipStat = this.ItemEquipStat.GetComponentInChildren<ShipItemEquipStat>();
        shipItemEquipStat.SetItem(itemToUplevel, playerData);
        shipItemEquipStat.ShowUgpradeEffect(itemToUplevel);
        AudioManager.Instance.SpawnSFX(SoundCode.LevelUpSound);
        this.ResetUI();
    }

    public virtual void OpenStat(ShipItemEquipData shipItemEquipData)
    {
        if (this.ItemEquipStat == null) return;
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        ShipItemEquipStat shipItemEquipStat = this.ItemEquipStat.GetComponentInChildren<ShipItemEquipStat>();
        shipItemEquipStat.SetItem(shipItemEquipData, playerData);
        UIManager.Instance.OpenElement(this.ItemEquipStat.gameObject);
        UIManager.Instance.OpenScaleUp(this.ItemEquipStat.gameObject, 0.25f);

    }
    public virtual void CloseStat()
    {
        if (this.ItemEquipStat == null) return;
        UIManager.Instance.CloseScaleUp(this.ItemEquipStat.gameObject, 0.25f);
    }
    public virtual void OpenEquipmentBar()
    {
        UIManager.Instance.OpenElement(this.gameObject);
        UIManager.Instance.CloseUIElementsExcept(this.gameObject);
        this.ResetUI();
    }
  
}

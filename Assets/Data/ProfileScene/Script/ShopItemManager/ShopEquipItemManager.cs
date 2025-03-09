using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEquipItemManager : FuncManager
{
    private static ShopEquipItemManager instance;
    public static ShopEquipItemManager Instance => instance;


    [Header("UI Item")]
    [SerializeField] protected ShopEquipItemSpawner shopEquipItemSpawner;
    [SerializeField] protected ShopEquipItemRefreshBtn shopEquipItemRefreshBtn;
    [SerializeField] protected ShopEquipItemRefreshTime ShopEquipItemRefreshTime;
    [SerializeField] protected Coroutine updateTimeCoroutine;
    protected override void Awake()
    {
        if (ShopEquipItemManager.instance != null) Debug.LogError("Only 1 ShopBarManager allowed to exist");
        ShopEquipItemManager.instance = this;
        UIManager.Instance.CloseElement(this.gameObject);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.SetShopUI();
        this.StartCoroutine(ShopRefreshTimer());

    }

    public virtual void SetShopUI()
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        this.shopEquipItemSpawner.SpawnItem(playerData.shopEquipItemDatas);
        this.shopEquipItemRefreshBtn.SetUI(playerData);
    }
    public virtual void RefreshShop()
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        if (playerData == null) return;
        playerData.shopEquipItemDatas.UpdateShopItemByDiamonds(playerData);
        this.SetShopUI();
    }
    public virtual void PurchaseItem(ShopEquipItemData shopEquiItemData)
    {
        if (shopEquiItemData == null) return;
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        if (playerData.shopEquipItemDatas.currentShopItems.Count <= 0) return;

        ShopEquipItemData selectShopItem = playerData.shopEquipItemDatas.GetShopItem(shopEquiItemData);
        if (shopEquiItemData == null) return;

        bool hasEnoughCurrency = playerData.currencyData.SpendCurrency(playerData, selectShopItem.GetGoldsData().Amount, selectShopItem.GetGoldsData().goldsData.CurrencyType);
        if (!hasEnoughCurrency) return;

        playerData.shipItemEquipDatas.AddItem(selectShopItem);
        selectShopItem.SetState(selectShopItem.shipItemData, true);
        PlayerDataLoad.Instance.SaveData();
    }



    private IEnumerator ShopRefreshTimer()
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) yield break;
        while (true)
        {
           
            string timer = playerData.shopEquipItemDatas.GetRemainingTimeForShopUpdate();
            this.ShopEquipItemRefreshTime.SetUI(timer);

            if (playerData.shopEquipItemDatas.UpdateShopItemsByTime())
            {
                this.SetShopUI();
            } 
            yield return new WaitForSeconds(1f);

        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShopEquipItemSpawner();
        this.LoadShopEquipItemRefreshBtn();
        this.LoadShopEquipItemRefreshTime();
    }
    protected virtual void LoadShopEquipItemSpawner()
    {
        if (this.shopEquipItemSpawner != null) return;
        this.shopEquipItemSpawner = transform.GetComponent<ShopEquipItemSpawner>();
        Debug.Log(transform.name + "Load ShopEquipItemSpawner ", gameObject);
    }
    protected virtual void LoadShopEquipItemRefreshBtn()
    {
        if (this.shopEquipItemRefreshBtn != null) return;
        this.shopEquipItemRefreshBtn = transform.Find("Stat").GetComponentInChildren<ShopEquipItemRefreshBtn>();
        Debug.Log(transform.name + " Load ShopEquipItemRefeshtBtn ", gameObject);
    }
    protected virtual void LoadShopEquipItemRefreshTime()
    {
        if (this.ShopEquipItemRefreshTime != null) return;
        this.ShopEquipItemRefreshTime = transform.Find("Stat").GetComponentInChildren<ShopEquipItemRefreshTime>();
        Debug.Log(transform.name + " Load ShopEquipItemRefeshTime ", gameObject);
    }
}

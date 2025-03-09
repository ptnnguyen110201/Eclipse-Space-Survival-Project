using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBarManager : FuncManager
{
    private static ShopBarManager instance;
    public static ShopBarManager Instance => instance;

    [SerializeField] protected Transform ItemPuchaseStat;
    [SerializeField] protected Transform ItemPuchaseSuccesStat;
    protected override void Awake()
    {
        if (ShopBarManager.instance != null) Debug.LogError("Only 1 ShopBarManager allowed to exist");
        ShopBarManager.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPurchaseStat();
    }

    protected virtual void LoadPurchaseStat() 
    {
        if (this.ItemPuchaseStat != null) return;
        this.ItemPuchaseStat = transform.Find("ItemPuchaseStat").GetComponent<Transform>();
        this.ItemPuchaseSuccesStat = transform.Find("ItemPuchaseSuccesStat").GetComponent<Transform>();

        this.ItemPuchaseStat.gameObject.SetActive(false);
        this.ItemPuchaseSuccesStat.gameObject.SetActive(false);
        Debug.Log(transform.name + " Load PurchaseStat", gameObject);
    }
    public void OpenStat(ShopEquipItemData ShopEquipItemData) 
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (this.ItemPuchaseStat == null) return;
        UIManager.Instance.OpenScaleUp(this.ItemPuchaseStat.gameObject, 0.5f);
        ShopPurchaseStat shopPurchaseStat = this.ItemPuchaseStat.GetComponentInChildren<ShopPurchaseStat>();
        shopPurchaseStat.SetItem(ShopEquipItemData, playerData);
    }
    public void OpenSuccesStat(ShopEquipItemData shopEquiItemData)
    {
        if (this.ItemPuchaseSuccesStat == null) return;
        UIManager.Instance.OpenScaleUp(this.ItemPuchaseSuccesStat.gameObject, 0.5f);
        ShopPurchaseCussesStat shopPurchaseCussesStat = this.ItemPuchaseSuccesStat.GetComponentInChildren<ShopPurchaseCussesStat>();
        shopPurchaseCussesStat.ShowUI(shopEquiItemData);
        AudioManager.Instance.SpawnSFX(SoundCode.RewardSound);
    }
    public void CloseStat()
    {
        if (this.ItemPuchaseStat == null || this.ItemPuchaseSuccesStat == null) return;
        this.ItemPuchaseSuccesStat.gameObject.SetActive(false);  
        this.ItemPuchaseStat.gameObject.SetActive(false);
    }
    public void OpenShop()
    {
        UIManager.Instance.OpenElement(this.gameObject);
        UIManager.Instance.CloseUIElementsExcept(this.gameObject);
        this.OpenElement();
    }

    private void OpenElement() 
    {
        UIManager.Instance.OpenElement(ShopEquipItemManager.Instance.gameObject);
    }

}

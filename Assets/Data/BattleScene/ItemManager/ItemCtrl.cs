    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : SelectableCtrl
{
    [SerializeField] protected ItemModel itemModel;
    [SerializeField] protected ItemSender itemSender;
    [SerializeField] protected ItemMove itemMove;
    protected override void LoadComponents()
    {
        this.LoadSelectAble();
        this.LoadItemModel();
        this.LoadItemSender();
        this.LoadItemMove();
        
    }
    protected virtual void LoadItemModel() 
    {
        if (this.itemModel != null) return;
        this.itemModel = transform.GetComponentInChildren<ItemModel>();
        this.itemModel.SetSprite(this.selectable.itemSprite);
        Debug.Log(transform.name + "Load ItemModel", gameObject);
    }
    protected virtual void LoadItemSender()
    {
        if (this.itemSender != null) return;
        this.itemSender = transform.GetComponentInChildren<ItemSender>();
        Debug.Log(transform.name + " Load ItemSender", gameObject);
    }
    protected virtual void LoadItemMove()
    {
        if (this.itemMove != null) return;
        this.itemMove = transform.GetComponentInChildren<ItemMove>();
        Debug.Log(transform.name + " Load ItemMove", gameObject);
    }
    protected override void LoadSelectAble()
    {
        if (this.selectable != null) return;
        string resPath = "ItemPickup/" + transform.name;
        this.selectable = Resources.Load<Selectable>(resPath);
        Debug.LogWarning(transform.name + ": Load ShipSO" + resPath);

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemManager.Instance?.RegisterItem(this);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ItemManager.Instance?.UnregisterItem(this);
    }

    public ItemSender GetItemSender() => this.itemSender;
    public ItemMove GetItemMove() => this.itemMove;
    public ItemModel GetItemModel() => this.itemModel;
}

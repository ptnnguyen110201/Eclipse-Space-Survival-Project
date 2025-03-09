using UnityEngine;

public class ItemSender : FuncManager
{
    [SerializeField] protected ItemCtrl ItemCtrl;
    [SerializeField] protected int amount;

    public int GetAmount() => this.amount;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemCtrl();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemData itemData = this.ItemCtrl.GetSelectable() as ItemData;
        this.amount = itemData.Amount;
    }
    protected virtual void LoadItemCtrl()
    {
        if (this.ItemCtrl != null) return;
        this.ItemCtrl = transform.parent.GetComponent<ItemCtrl>();
        Debug.Log(transform.name + ": LoadItemCtrl", gameObject);
    }
    public virtual void Send(Transform obj)
    {
        ItemData itemData = this.ItemCtrl.GetSelectable() as ItemData;
        if (itemData == null) 
        {
            Debug.Log("ItemData is Null");
            return; 
        }
        switch (itemData.itemType)
        {
            case ItemType.Health:
                DamageReceiver HPReceiver = obj.GetComponentInChildren<DamageReceiver>();
                if (HPReceiver == null)
                {
                    return;
                }
                this.SendHP(HPReceiver);
                break;
            case ItemType.Exp:
                ShipLevel shipLevel = obj.GetComponentInChildren<ShipLevel>();
                if (shipLevel == null)
                {
                    return;
                }
                this.SendExp(shipLevel);
                break;
            case ItemType.Boom:
                DamageReceiver ATKReceiver = obj.GetComponentInChildren<DamageReceiver>();
                if (ATKReceiver == null)
                {
                    return;
                }
                this.SendATK(ATKReceiver);
                break;
            case ItemType.Gold:
                MapStatistics mapStatistics = MapStatistics.Instance;
                if (mapStatistics == null)
                {
                    return;
                }
                this.SendGolds(mapStatistics);
                break;


        }
        AudioManager.Instance.SpawnSFX(SoundCode.PickupSound);
        this.DespawnItem();
    }


    public virtual void SendATK(DamageReceiver damageReceiver)
    {
        if (damageReceiver == null)
        {
            return;
        }
        double maxHp = damageReceiver.HPMax;
        if (damageReceiver.transform.parent.CompareTag("Enemy")) 
        {
            double sendAmount = (this.amount * maxHp) / 100f;
            damageReceiver.Deduct(sendAmount);
            return;
        }
        if (damageReceiver.transform.parent.CompareTag("Boss")) 
        {
            double sendAmount = (this.amount * maxHp) / 400f;
            damageReceiver.Deduct(sendAmount);
            return;
        }
    }
    public virtual void SendExp(ShipLevel Shiplevel) => Shiplevel.AddEXP(this.amount);
    public virtual void SendHP(DamageReceiver damageReceiver) => damageReceiver.Add(this.amount);
    public virtual void SendGolds(MapStatistics mapStatistics) => mapStatistics.AddEarnings(this.amount);
    protected virtual void DespawnItem() => ItemPickupSpawner.Instance.Despawn(transform.parent);
    
    public void SetAmount(int amount) => this.amount = amount;

 
}

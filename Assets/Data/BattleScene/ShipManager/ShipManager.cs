using System;
using UnityEngine;

public class ShipManager : FuncManager
{
    private static ShipManager instance;
    public static ShipManager Instance => instance;

    [SerializeField] protected ShipSpawner shipSpawner; 
    [SerializeField] protected ShipCtrl currentShip;   

    public event Action OnShipDead;

    protected override void Awake()
    {
        if (ShipManager.instance != null && ShipManager.instance != this)
        {
            Debug.LogWarning("Another ShipManager instance already exists. Destroying this instance.");
            Destroy(this.gameObject);
            return;
        }

        ShipManager.instance = this;
        base.Awake(); 
        this.SpawnShip();
    }




  

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipSpawner();
    }

    public void NotifyShipDead()
    {
        this.OnShipDead?.Invoke();
    }
    public void Respawn() 
    {

        Vector3 Rot = this.currentShip.transform.position;
        Transform newShip = this.shipSpawner.Respawn(Rot);
        if (newShip == null)
        {
            Debug.LogError(transform.name + " Ship can't spawn", gameObject);
            return;
        }

        ShipCtrl shipCtrl = newShip.GetComponent<ShipCtrl>();
        if (shipCtrl == null)
        {
            Debug.LogError(transform.name + " ShipCtrl is Null", gameObject);
            return;
        }

        this.currentShip = shipCtrl;

        if (ExpBarManager.Instance != null)
        {
            ExpBarManager.Instance.SetShipCtrl(shipCtrl);
        }

        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.SetPlayerPos(shipCtrl.transform);
        }
    }
    public void SpawnShip()
    {

        Transform newShip = this.shipSpawner.SpawnShip();
        if (newShip == null)
        {
            Debug.LogError(transform.name + " Ship can't spawn", gameObject);
            return;
        }

        ShipCtrl shipCtrl = newShip.GetComponent<ShipCtrl>();
        if (shipCtrl == null)
        {
            Debug.LogError(transform.name + " ShipCtrl is Null", gameObject);
            return;
        }

        this.currentShip = shipCtrl;

        if (ExpBarManager.Instance != null)
        {
            ExpBarManager.Instance.SetShipCtrl(shipCtrl);
        }

        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.SetPlayerPos(shipCtrl.transform);
        }
     
    }

    protected virtual void LoadShipSpawner()
    {
        if (this.shipSpawner != null) return;

        this.shipSpawner = transform.GetComponent<ShipSpawner>();
        if (this.shipSpawner == null)
        {
            Debug.LogError(transform.name + " ShipSpawner not found!", gameObject);
        }
        else
        {
            Debug.Log(transform.name + " Load ShipSpawner", gameObject);
        }
    }
    public ShipCtrl GetShipCtrl()
    {
        if (this.currentShip == null)
        {
            Debug.LogWarning("Current Ship is null.");
        }
        return this.currentShip;
    }
    public ShipSpawner GetShipSpawner()
    {
        if (this.shipSpawner == null)
        {
            Debug.LogError("ShipSpawner is null.");
        }
        return this.shipSpawner;
    }
}

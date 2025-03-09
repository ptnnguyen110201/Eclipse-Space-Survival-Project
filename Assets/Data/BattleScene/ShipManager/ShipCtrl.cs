using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCtrl : FuncManager
{


    [Header("Body")]
    [SerializeField] protected Rigidbody2D ShipRb2D;
    [SerializeField] protected CircleCollider2D CircleCollider2D;

    [Header("Ability")]
    [SerializeField] protected ShipModel shipModel;
    [SerializeField] protected ShipArea shipArea;
    [SerializeField] protected ShipLookAt shipLookAt;
    [SerializeField] protected ShipDamageReceiver shipDamageReceiver;
    [SerializeField] protected ShipLevel shipLevel;
    [SerializeField] protected ShipAbility shipAbility;
    [Header("Data")]
    [SerializeField] protected ShipData shipSO;
    [SerializeField] protected ShipAttributes shipAttributes;
    [SerializeField] protected ShipAttributes baseShipAttributes;
    protected override void OnEnable()
    {
        base.OnEnable();
        this.LoadShipData();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipSO();

        this.LoadRigibody();
        this.LoadCollider();

        this.LoadShipModel();
        this.LoadShipArea();
        this.LoadShipLookAt();
        this.LoadShipDamageReceiver();
        this.LoadShipLevel();
        this.LoadShipAbility();
    }

    protected virtual void LoadRigibody()
    {
        if (this.ShipRb2D != null) return;
        this.ShipRb2D = transform.GetComponent<Rigidbody2D>();
        this.ShipRb2D.gravityScale = 0;
        Debug.Log(transform.name + "Load RigiBody ", gameObject);
    }
    protected virtual void LoadCollider()
    {
        if (this.CircleCollider2D != null) return;
        this.CircleCollider2D = transform.GetComponent<CircleCollider2D>();
        Debug.Log(transform.name + "Load RigiBody ", gameObject);
    }
    protected virtual void LoadShipModel()
    {
        if (this.shipModel != null) return;
        this.shipModel = transform.GetComponentInChildren<ShipModel>();
        Debug.Log(transform.name + "Load ShipModel", gameObject);
    }

    protected virtual void LoadShipArea()
    {
        if (this.shipArea != null) return;
        this.shipArea = transform.GetComponentInChildren<ShipArea>();
        Debug.Log(transform.name + "Load ShipArea", gameObject);
    }
    protected virtual void LoadShipLookAt()
    {
        if (this.shipLookAt != null) return;
        this.shipLookAt = transform.GetComponentInChildren<ShipLookAt>();
        Debug.Log(transform.name + "Load ShipLookAt", gameObject);
    }
    protected virtual void LoadShipDamageReceiver()
    {
        if (this.shipDamageReceiver != null) return;
        this.shipDamageReceiver = transform.GetComponentInChildren<ShipDamageReceiver>();
        Debug.Log(transform.name + "Load ShipLookAt", gameObject);
    }
    protected virtual void LoadShipLevel()
    {
        if (this.shipLevel != null) return;
        this.shipLevel = transform.GetComponentInChildren<ShipLevel>();
        Debug.Log(transform.name + "Load ShipLevel", gameObject);
    }

    protected virtual void LoadShipAbility()
    {
        if (this.shipAbility != null) return;
        this.shipAbility = transform.GetComponentInChildren<ShipAbility>();
        Debug.Log(transform.name + "Load ShipAbility", gameObject);
    }
    protected virtual void LoadShipData()
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null)
        {
            Debug.Log("PlayData is null");
            return;
        }
        this.shipSO.SetImage(playerData);
        double ATK = playerData.shipItemEquipDatas.currentATK;
        double HP = playerData.shipItemEquipDatas.currentHP;

        this.shipAttributes.SetAttributes(HP, ATK);
        this.baseShipAttributes.SetAttributes(HP, ATK);
        Debug.Log(transform.name + "Load ShipData", gameObject);
    }
    protected virtual void LoadShipSO()
    {
        if (this.shipSO != null) return;
        string resPath = "ShipDatas/ShipData";
        this.shipSO = Resources.Load<ShipData>(resPath);
        Debug.LogWarning(transform.name + ": Load ShipSO" + resPath);
    }

    public Rigidbody2D GetRigidbody2D() => this.ShipRb2D;
    public CircleCollider2D GetCircleCollider2D() => this.CircleCollider2D;
    public ShipData GetShipData() => this.shipSO;
    public ShipModel GetShipModel() => this.shipModel;
    public ShipArea GetShipArea() => this.shipArea;
    public ShipLookAt GetShipLookAt() => this.shipLookAt;
    public ShipDamageReceiver GetShipDamageReceiver() => this.shipDamageReceiver;
    public ShipLevel GetShipLevel() => this.shipLevel;
    public ShipAttributes GetShipAttributes() => this.shipAttributes;
    public ShipAttributes GetBaseShipAttributes() => this.baseShipAttributes;
    public ShipAbility GetShipAbility() => this.shipAbility;
}

using UnityEngine;

public class BulletCtrl : FuncManager
{
    [SerializeField] protected Transform BulletObject;
    [SerializeField] protected BulletModel BulletModel;
    [SerializeField] protected Despawner Despawner;
    [SerializeField] protected DamageSender DamageSender;
    [SerializeField] protected MoveBase moveBase;
    [SerializeField] protected BulletImpart bulletImpart;
    [SerializeField] protected Transform shooter;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletObject();
        this.LoadBulletModel();
        this.LoadDespawner();
        this.LoadDamageSender();
        this.LoadBulletMove();
        this.LoadBulletImpart();
    }
    public virtual void SetBullet(AbilityAttributes abilityAttributes, Sprite BulletSprite, Transform shooter)
    {
        if (abilityAttributes == null) return;
        this.shooter = shooter;
        this.BulletModel.SetBulletSprite(BulletSprite);
        this.DamageSender.SetATK(abilityAttributes.ATK);
        this.DamageSender.SetSizeArea(abilityAttributes.SizeArea);
        this.DamageSender.SetCritRate(abilityAttributes.CritRate);
        this.DamageSender.SetCritDamage(abilityAttributes.CritDamage);
        this.SetSize(abilityAttributes.SizeArea);
    }
    public virtual void SetBullet(ObjectAttribute objectAttribute, Transform shooter)
    {
        if (objectAttribute == null) return;
        this.shooter = shooter;
        this.DamageSender.SetATK(objectAttribute.objectATK);

    }
    protected virtual void LoadBulletObject()
    {
        if (this.BulletObject != null) return;
        this.BulletObject = transform.GetComponent<Transform>();
        Debug.Log(transform.name + ": LoadBulletObject", gameObject);
    }
    protected virtual void LoadBulletModel()
    {
        if (this.BulletModel != null) return;
        this.BulletModel = transform.GetComponentInChildren<BulletModel>();
        Debug.Log(transform.name + ": LoadBulletModel", gameObject);
    }
    protected virtual void LoadDespawner()
    {
        if (this.Despawner != null) return;
        this.Despawner = transform.GetComponentInChildren<Despawner>();
        Debug.Log(transform.name + ": LoadDespawner", gameObject);
    }
    protected virtual void LoadDamageSender()
    {
        if (this.DamageSender != null) return;
        this.DamageSender = transform.GetComponentInChildren<DamageSender>();
        Debug.Log(transform.name + ": LoadDamageSenderr", gameObject);
    }
    protected virtual void LoadBulletImpart()
    {
        if (this.bulletImpart != null) return;
        this.bulletImpart = transform.GetComponentInChildren<BulletImpart>();
        Debug.Log(transform.name + ": LoadBulletImpart", gameObject);
    }
    protected virtual void LoadBulletMove()
    {
        if (this.moveBase != null) return;
        this.moveBase = transform.GetComponentInChildren<MoveBase>();
        Debug.Log(transform.name + ": LoadBulletMove", gameObject);
    }

    public BulletModel GetBulletModel() => this.BulletModel;
    public Despawner GetDespawner() => this.Despawner;
    public DamageSender GetDamageSender() => this.DamageSender;
    public BulletImpart GetBulletImpart() => this.bulletImpart;
    public MoveBase GetMoveBase() => this.moveBase;
    public Transform GetShooter() => this.shooter;
    public virtual void SetSize(float Size)
    {
        if (Size <= 0) return;
        this.BulletObject.localScale = new Vector3(Size / 2f, Size / 2f, 0);
    }
}

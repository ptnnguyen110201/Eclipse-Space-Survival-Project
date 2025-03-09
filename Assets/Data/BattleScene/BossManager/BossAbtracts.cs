using UnityEngine;

public abstract class BossAbtracts : FuncManager
{
    [Header("Bullet Abtract")]
    [SerializeField] protected BossCtrl bossCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossCtrl();
    }

    protected virtual void LoadBossCtrl()
    {
        if (this.bossCtrl != null) return;
        this.bossCtrl = transform.parent.GetComponent<BossCtrl>();
        Debug.Log(transform.name + ":Load BossCtrl", gameObject);
    }

    public BossCtrl GetBossCtrl() => this.bossCtrl;
}

using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BossHPBarUI : FuncManager
{
    private static BossHPBarUI instance;
    public static BossHPBarUI Instance => instance;

    [SerializeField] private BossHPSlider bossHPSlider;
    [SerializeField] protected BossCtrl bossCtrl;

    public event Action<double, double> OnHealthChanged;

    public void SetBossCtrl(BossCtrl bossCtrl)
    {
        if (bossCtrl == null) return;
        this.bossCtrl = bossCtrl;
        this.bossCtrl.OnHealthChanged += UpdateHealthBar;
        this.bossCtrl.OnBossDeath += ShowBossHPBar;
        this.bossCtrl.OnBossDeath += HideBossHPBar;
    }


    private void UpdateHealthBar(double currentHP, double maxHP)
    {

        this.OnHealthChanged?.Invoke(currentHP, maxHP);

        if (this.bossHPSlider != null)
        {
            this.bossHPSlider.SetCurrentValue(currentHP);
            this.bossHPSlider.SetMaxValue(maxHP);
        }
    }

    protected override void Awake()
    {
        if (BossHPBarUI.instance != null) Debug.LogError("Only 1 BossHPBarUI allowed to exist");
        BossHPBarUI.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.gameObject.SetActive(false);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossHPSlider();
    }

    protected virtual void LoadBossHPSlider() 
    {
        if (this.bossHPSlider != null) return;
        this.bossHPSlider = transform.GetComponentInChildren<BossHPSlider>();
        Debug.Log(transform.name + " LoadBossHPSlider", gameObject);
    }
    public void ShowBossHPBar()
    {
        if (bossHPSlider != null)
        {
            this.gameObject.SetActive(true);
        }
    }

    public void HideBossHPBar()
    {
        if (this.bossHPSlider != null)
        {
            this.gameObject.SetActive(false);
        }
    }


    protected void OnDestroy()
    {
        if (this.bossCtrl != null)
        {
            this.bossCtrl.OnHealthChanged -= UpdateHealthBar;
            this.bossCtrl.OnBossDeath -= ShowBossHPBar;
            this.bossCtrl.OnBossDeath -= HideBossHPBar;
        }
    }
}

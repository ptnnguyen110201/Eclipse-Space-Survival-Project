using System.Collections;
using UnityEngine;

public class MenuManager : FuncManager
{
    private static MenuManager instance;
    public static MenuManager Instance => instance;
    [SerializeField] protected Transform SettingPanel;
    [SerializeField] protected ReviveMenu ReviveMenu;
    public ReviveMenu GetReviveMenu() => this.ReviveMenu;
    protected override void Awake()
    {
        if (MenuManager.instance != null) Debug.LogError("Only 1 MenuManager allowed to exist");
        MenuManager.instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSettingPanel();
        this.LoadReviveMenu();
    }
    protected override void Start()
    {
        base.Start();
        UIManager.Instance.CloseElement(this.SettingPanel.gameObject);
        UIManager.Instance.CloseElement(this.ReviveMenu.gameObject);
    }
    public void OpenMenu()
    {
        UIManager.Instance.OpenScaleUp(this.SettingPanel.gameObject, 0.25f);
        Time.timeScale = 0f;

    }
    public void CloseMenu()
    {
        UIManager.Instance.CloseScaleUp(this.SettingPanel.gameObject, 0.25f);
        Time.timeScale = 1f;

    }

    public void OpenReviveMenu() 
    {
        UIManager.Instance.OpenScaleUp(this.ReviveMenu.gameObject, 0.25f);
        Time.timeScale = 0f;

    }
    public void CloseReviveMenu()
    {
        UIManager.Instance.CloseScaleUp(this.ReviveMenu.gameObject, 0.25f);
        Time.timeScale = 1f;

    }

    protected virtual void LoadSettingPanel() 
    {
        if (this.SettingPanel != null) return;
        this.SettingPanel = transform.Find("PanelBG").GetComponent<Transform>();
        Debug.Log(transform.name + "Load SettingPanel", gameObject);
    }
    protected virtual void LoadReviveMenu()
    {
        if (this.ReviveMenu != null) return;
        this.ReviveMenu = transform.Find("ReviveMenu").GetComponent<ReviveMenu>();
        Debug.Log(transform.name + "Load ReviveMenu", gameObject);
    }
}

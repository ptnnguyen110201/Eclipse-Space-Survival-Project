using UnityEngine;
public class SettingsManager : FuncManager
{
    protected override void Start()
    {
        base.Start();
        this.transform.gameObject.SetActive(false);
    }
    public void OpenSettings() 
    {
        UIManager.Instance.OpenScaleUp(this.transform.gameObject, 0.5f);
    }
    public void CloseSettings()
    {
        UIManager.Instance.CloseScaleUp(this.transform.gameObject, 0.5f);
    }
}
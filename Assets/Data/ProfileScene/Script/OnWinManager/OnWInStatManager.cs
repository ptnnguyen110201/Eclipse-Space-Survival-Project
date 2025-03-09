using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWInStatManager : FuncManager
{
    [SerializeField] protected RewardPopUp rewardPopUp;
    protected override void Start()
    {
        base.Start();
        this.transform.gameObject.SetActive(false);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRewardPopUp();
    }

    protected virtual void LoadRewardPopUp()
    {
        if (this.rewardPopUp != null) return;
        this.rewardPopUp = transform.GetComponentInChildren<RewardPopUp>();
        Debug.Log(transform.name + "LoadRewardPopUp", gameObject);
    }
    public void Close()
    {
        UIManager.Instance.CloseScaleUp(this.transform.gameObject, 0.5f);
    }
    public void OpenStat(PlayerData playerData, bool Checkwin)
    {
        if (playerData == null) return;

        UIManager.Instance.OpenScaleUp(this.transform.gameObject, 0.5f);
        this.rewardPopUp.ShowUI(playerData, Checkwin);

        AudioManager.Instance.SpawnSFX(SoundCode.WinSound);

    }

}

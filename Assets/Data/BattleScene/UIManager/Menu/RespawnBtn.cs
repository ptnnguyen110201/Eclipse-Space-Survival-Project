using UnityEngine;
public class RespawnBtn : ButtonBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        this.SetBtn();
    }

    protected virtual void SetBtn()
    {
        int PlayerDiamond = PlayerDataLoad.Instance.GetPlayerData().currencyData.playerDiamonds.Amount;
        int ReviveCoins = MapStatistics.Instance.GetMapStatisticsData().ReviveCoins.Amount;

        this.button.interactable = PlayerDiamond > ReviveCoins;
    }
    protected override void OnClick()
    {
        MenuManager.Instance.GetReviveMenu().Respawn();
        MenuManager.Instance.CloseReviveMenu();
    }
}
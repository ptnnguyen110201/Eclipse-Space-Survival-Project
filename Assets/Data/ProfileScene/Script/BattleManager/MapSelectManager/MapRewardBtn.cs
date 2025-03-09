using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRewardBtn : ButtonBase
{
    [SerializeField] protected Transform ChestIcon;
    [SerializeField] protected Transform ChestLocked;
    [SerializeField] protected MapResult mapResult;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadChestIcon();
        this.LoadChestLocked();
    }
    public void ShowUI(MapResult mapResult)
    {
        if (mapResult == null)
        {
            Debug.LogWarning("MapResult is null.");
            return;
        }

        this.mapResult = mapResult;

        if (mapResult.IsCompleted && !mapResult.IsRewardClaimed)
        {
            this.button.interactable = true;
            this.ChestIcon.gameObject.SetActive(true);
            this.ChestLocked.gameObject.SetActive(false);
        }
        else if (mapResult.IsRewardClaimed)
        {
            this.button.interactable = false;
            this.ChestIcon.gameObject.SetActive(true);
            this.ChestLocked.gameObject.SetActive(true);
        }
        else
        {
            this.button.interactable = false;
            this.ChestIcon.gameObject.SetActive(true);
            this.ChestLocked.gameObject.SetActive(false);
        }
    }
    protected virtual void LoadChestIcon() 
    {
        if (this.ChestIcon != null) return;
        this.ChestIcon = transform.Find("ChestIcon").GetComponent<Transform>();
        Debug.Log(transform.name + " LoadChestIcon", gameObject);
    }
    protected virtual void LoadChestLocked()
    {
        if (this.ChestLocked != null) return;
        this.ChestLocked = transform.Find("ChestLocked").GetComponent<Transform>();
        Debug.Log(transform.name + " LoadChestIcon", gameObject);
    }
    protected override void OnClick()
    {
        BattleManager.Instance.GetMapManager().RewardMap();
        this.ShowUI(mapResult);
    }
}

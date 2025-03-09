using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : FuncManager
{
    [SerializeField] protected MapSelectSpawner mapSelectSpawner;
    [SerializeField] protected MapSelectBar mapSelectBar;
    [SerializeField] protected Transform Warining;
    public MapSelectBar GetMapSelectBar() => this.mapSelectBar;

    protected override void LoadComponents()
    {
        base.LoadComponents();  
        this.LoadMapSelectBar();
        this.LoadMapSpawner();
      
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.StopAllCoroutines();
        this.Warining.gameObject.SetActive(false);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.SpawnMap();
    }
    protected virtual void LoadMapSelectBar()
    {
        if (this.mapSelectBar != null) return;
        this.mapSelectBar = transform.GetComponentInChildren<MapSelectBar>();
        Debug.Log(transform.name + "Load MapSelectBar", gameObject);
    }
    protected virtual void LoadMapSpawner()
    {
        if (this.mapSelectSpawner != null) return;
        this.mapSelectSpawner = transform.GetComponentInChildren<MapSelectSpawner>();
        this.Warining = transform.Find("Warning").GetComponent<Transform>();
        Debug.Log(transform.name + " Load MapSpawner", gameObject);
    }
    public bool PlayWithMap(MapResult mapResult)
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();

        if (playerData == null)
        {
            Debug.LogWarning("Player data is null. Cannot play map.");
            return false;
        }

        if (!playerData.shipItemEquipDatas.hasEquipItem())
        {
            Debug.LogWarning("Player has not equipped any item. Cannot play map.");
            this.StartCoroutine(ShowWarning("Player has not equipped any item!"));
            return false;
        }

        if (!playerData.mapProgress.DeductEnergyToPlayMap(playerData))
        {
            Debug.LogWarning("Player energy is not enough. Cannot play map.");
            this.StartCoroutine(ShowWarning("Player energy is not enough!"));
            return false;
        }

        playerData.gameState.SelectedMapData = mapResult;
        playerData.questDatas.UpdateQuestProgress(QuestType.PlayTotalGames, 1);
        return true;
    }
    private IEnumerator ShowWarning(string warningText)
    {
        if (this.Warining == null) yield break;
        TextMeshProUGUI textMeshProUGUI = this.Warining.GetComponentInChildren<TextMeshProUGUI>(true);
        textMeshProUGUI.text = warningText;
        this.Warining.gameObject.SetActive(true);  
        yield return new WaitForSeconds(2f);
        this.Warining.gameObject.SetActive(false);

    }
    public void RewardMap() 
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null)
        {
            Debug.LogWarning("Player data is null. Cannot unlock map.");
            return;
        }

        playerData.gameState.SelectedMapData.ClaimReward(playerData);
    }
    public void RewardMapEnd(int TotalGoldCount)
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null)
        {
            Debug.LogWarning("Player data is null. Cannot unlock map.");
            return;
        }

        playerData.currencyData.AddCurrency(playerData, TotalGoldCount, CurrencyType.Golds);
        playerData.gameState.MapStatistics.UpdateQuestProgress(playerData);
    }
    public void UnlockMap(bool CheckWin)
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null)
        {
            Debug.LogWarning("Player data is null. Cannot unlock map.");
            return;
        }

        if (!CheckWin) return;

        int nextMapID = playerData.gameState.SelectedMapData.mapdata.MapID ;
        Debug.Log($"Next Map ID: {nextMapID}");

        if (nextMapID < 0 || nextMapID >= playerData.mapProgress.mapResults.Count)
        {
            Debug.LogWarning($"Invalid Map ID: {nextMapID}. Cannot unlock map.");
            return;
        }

        MapResult nextMap = playerData.mapProgress.mapResults[nextMapID];
        Debug.Log($"Next Map: {nextMap}");

        if (nextMap == null || nextMap.IsUnlocked) return;

        nextMap.SetMapUnlocked();
        PlayerDataLoad.Instance.SaveData();
    }
    public void Complete(bool CheckWin)
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();

        if (!CheckWin)
        {
            Debug.LogWarning("Player has not won the battle. Cannot complete map.");
            return;
        }
        playerData.gameState.SelectedMapData.IsCompleted = true;
        PlayerDataLoad.Instance.SaveData();
       
    }

    public void SpawnMap() 
    {
        PlayerData playerData = PlayerDataLoad.Instance.GetPlayerData();
        if (playerData == null) return;
        List<MapResult> results = playerData.mapProgress.mapResults;
        this.mapSelectSpawner.SpawnMap(results);
        this.mapSelectBar.LoadContent();
    }
    

}

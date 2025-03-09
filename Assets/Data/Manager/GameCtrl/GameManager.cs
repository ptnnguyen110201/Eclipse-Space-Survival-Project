using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using System;
public class GameManager : FuncManager
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    public event Action<bool> OnGameEnd; 
    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);    
        Application.targetFrameRate = 120;
        if (Application.platform == RuntimePlatform.Android)
        {
            Screen.SetResolution(1080, 1920, true);
            return;
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(540, 960, false);
            return;
        }


    }

    public PlayerData GetPlayerData() => PlayerDataLoad.Instance.GetPlayerData();
    protected override void Start()
    {
        base.Start();
        DOTween.SetTweensCapacity(10000, 200);
        AudioManager.Instance.SpawnSFXByScene("ProfileScene");
    }
    public void TriggerGameEnd(bool isWin)
    {
        this.OnGameEnd?.Invoke(isWin);
        this.HandleGameEnd(isWin);
    }

    private void HandleGameEnd(bool isWin)
    {
        DOTween.KillAll();
        this.StartCoroutine(LoadProfileScene(isWin));
    }
    private IEnumerator LoadProfileScene(bool isWin)
    {
        PlayerDataLoad.Instance.GetPlayerData().gameState.SetMapStatisticsData(MapStatistics.Instance.GetMapStatisticsData());
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("ProfileScene");
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }
        asyncOperation.allowSceneActivation = true;
        yield return new WaitUntil(() => asyncOperation.isDone);

        AudioManager.Instance.SpawnSFXByScene("ProfileScene");   
        BattleManager.Instance.GetWInStatManager().OpenStat(this.GetPlayerData(), isWin);

    }

    public IEnumerator LoadBattleScene(MapResult mapResult)
    {

        if (!BattleManager.Instance.GetMapManager().PlayWithMap(mapResult))
        {
            Debug.LogWarning("Cannot load BattleScene because PlayWithMap returned false.");
            yield break;
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("BattleScene");
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        asyncOperation.allowSceneActivation = true;   
        AudioManager.Instance.SpawnSFXByScene("BattleScene");
        yield return new WaitUntil(() => asyncOperation.isDone);

    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
            this.ExitGame();
        }
    }
    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        SaveSystem.SavePlayerData(this.GetPlayerData());
        Application.Quit();
    }
}

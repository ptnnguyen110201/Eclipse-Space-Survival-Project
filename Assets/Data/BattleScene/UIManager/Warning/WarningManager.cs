using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class WarningManager : FuncManager
{
    private static WarningManager instance;
    public static WarningManager Instance => instance;

    [SerializeField] protected WarningBar warningBar;
    [SerializeField] protected StartBar startBar;
    [SerializeField] protected Transform mask;
    private Coroutine blinkCoroutine;

    protected override void Awake()
    {
        if (WarningManager.instance != null) Debug.LogError("Only 1 WarningManager allowed to exist");
        WarningManager.instance = this;
    }
    protected override void Start()
    {
        base.Start();
        this.warningBar.gameObject.SetActive(false);
    }
    protected virtual void LoadWarningBar()
    {
        if (this.warningBar != null || this.startBar != null) return;
        this.warningBar = transform.GetComponentInChildren<WarningBar>();
        this.startBar = transform.GetComponentInChildren<StartBar>();
        this.mask = transform.Find("Mask").GetComponent<Transform>();
        this.mask.gameObject.SetActive(false);
        Debug.Log(transform.name + " Load WarningBar", gameObject);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWarningBar();
    }
    public IEnumerator OnStartGame()
    {
        if (this.startBar == null) yield break;
        this.mask.gameObject.SetActive(true);
        this.startBar.gameObject.SetActive(true);  
        AudioManager.Instance.SpawnSFX(SoundCode.CountdownSound);
        for (int i = 3; i > 0; i--)
        {
            this.startBar.SetStartText(i.ToString());
            yield return new WaitForSeconds(1f);
        }
        this.startBar.SetStartText("GO!");
        yield return new WaitForSeconds(1f);
        this.startBar.gameObject.SetActive(false);
    }


    public void OnWarning(bool warning = false, bool isBossWave = false)
    {
        if (this.blinkCoroutine != null)
        {
            this.StopCoroutine(blinkCoroutine);
            this.blinkCoroutine = null;
        }

        if (warning)
        {
          
            string warningText = isBossWave ? "boss incoming!" : "enemy acceleration!";
            this.warningBar.SetWarningText(warningText); 
           
            this.blinkCoroutine = this.StartCoroutine(BlinkWarningBar()); 
            AudioManager.Instance.SpawnSFX(SoundCode.WarningSound);
            return;
        }

        this.warningBar.gameObject.SetActive(false);

    }

    private IEnumerator BlinkWarningBar()
    {
        float blinkDuration = 0.5f;
        while (true)
        {
            this.warningBar.gameObject.SetActive(!this.warningBar.gameObject.activeSelf);
            yield return new WaitForSeconds(blinkDuration);
        }

    }
}

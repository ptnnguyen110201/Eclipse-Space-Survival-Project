using UnityEngine;

public class AudioManager : Spawner
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] protected int soundLimit = 20;
    protected override void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SpawnSFX(SoundCode soundCode)
    {
        if(soundCode == SoundCode.None) return;
        if(!this.SoundLimit(soundCode)) return;
        Transform newSFX = this.Spawn(soundCode.ToString(), transform.position, Quaternion.identity);
        newSFX.gameObject.SetActive(true);
    }

    public void SpawnSFXByScene(string SceneName)
    {
        this.ClearSound();
        if (SceneName == "ProfileScene")
        {
            Transform newSFX = this.Spawn(SoundCode.ProfileSound.ToString(), transform.position, Quaternion.identity);
            newSFX.gameObject.SetActive(true);
        }
        if (SceneName == "BattleScene")
        {
            Transform newSFX = this.Spawn(SoundCode.BattleSound.ToString(), transform.position, Quaternion.identity);
            newSFX.gameObject.SetActive(true);
        }
    }
    
    public void ClearSound() 
    {
        if (this.holder.childCount <= 0) return;
        foreach (Transform obj in this.holder)
        {
            this.Despawn(obj);
        }
    }
    public bool SoundLimit(SoundCode soundCode)
    {
        if (this.holder.childCount <= 0) return true;
        int AcctiveSound = 0;
        foreach (Transform obj in this.holder)
        {
            if(!obj.gameObject.activeSelf) continue;
            AudioCtrl audioCtrl = obj.GetComponent<AudioCtrl>();
            audioCtrl.audioData.soundCode = soundCode;
            AcctiveSound++;
        }
        return AcctiveSound < this.soundLimit;
    }
}

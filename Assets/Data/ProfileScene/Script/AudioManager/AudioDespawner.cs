

using System.Collections.Generic;
using UnityEngine;


public class AudioDespawner : DespawnByTime
{
    [SerializeField] protected AudioCtrl audioCtrl;
    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = this.audioCtrl.audioSource.clip.length;
    }
    public override void DespawnObject()
    {
        if (this.audioCtrl.audioSource.loop) return;
        AudioManager.Instance.Despawn(transform.parent);

    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioCtrl();
    }

    protected virtual void LoadAudioCtrl()
    {
        if (this.audioCtrl != null) return;
        this.audioCtrl = transform.parent.GetComponent<AudioCtrl>();
        Debug.Log(transform.name + ": Load AudioCtrl", gameObject);
    }

}

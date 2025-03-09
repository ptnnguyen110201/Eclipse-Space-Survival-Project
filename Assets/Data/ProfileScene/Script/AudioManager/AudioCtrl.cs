using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : FuncManager
{
    [SerializeField] public AudioData audioData;
    [SerializeField] public AudioSource audioSource;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioData();
        this.LoadAudioSource();
        this.SetAudioSource();
    }
    protected virtual void LoadAudioData()
    {
        if (this.audioData != null) return;
        string objectName = transform.name;
        this.audioData = Resources.Load<AudioData>($"Sound/{objectName}");
        Debug.Log(transform.name + " Load AudioData",gameObject);

    }
    protected virtual void LoadAudioSource()
    {
        if (this.audioSource != null) return;
        this.audioSource = transform.GetComponent<AudioSource>();
        Debug.Log(transform.name + " Load AudioSource", gameObject);

    }
    protected virtual void SetAudioSource() 
    {
        if (this.audioSource == null || this.audioData == null) return;
        this.audioSource.clip = this.audioData.clip;
        this.audioSource.volume = this.audioData.volume;
        this.audioSource.loop = this.audioData.loop;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.audioSource.Play();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        this.audioSource.Stop();
    }
}

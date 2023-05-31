using System.Collections;
using NTC.Global.System;
using UnityEngine;
using Plugins.Audio.Core;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singleton<MusicManager>
{
    
    private AudioSource _audioSource;
    private SourceAudio _sourceAudio;
    public float volume = 0.1f;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _sourceAudio = GetComponent<SourceAudio>();
        volume = (PlayerPrefs.HasKey("MUSIC_VOLUME")) ? PlayerPrefs.GetFloat("MUSIC_VOLUME") : volume;
        _audioSource.volume = volume;
        //_sourceAudio.Play("MainMusic");
        DontDestroyOnLoad(transform.gameObject);
        StartCoroutine(PlayMusic());

    }
    
    public void ChangeVolume(float newVolume)
    {
        volume = newVolume;
        _audioSource.volume = volume;
    }
    private IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(0.2f);
        _sourceAudio.Play("MainMusic");
    }
}

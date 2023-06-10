using NTC.Global.System;
using UnityEngine;
using Plugins.Audio.Core;

public class MusicManager : Singleton<MusicManager>
{
    public enum MusicType { MainMenuMusic = 0, InGameMusic };

    private SourceAudio _sourceAudio;
    public float volume = 0.1f;

    [SerializeField]private float volumeK = 0.7f;

    void Start()
    {
        _sourceAudio = GetComponent<SourceAudio>();
        volume = (PlayerPrefs.HasKey("MUSIC_VOLUME")) ? PlayerPrefs.GetFloat("MUSIC_VOLUME") : volume;
        volume *= volumeK;

        _sourceAudio.Volume = volume;
        DontDestroyOnLoad(transform.gameObject);
        PlayMusic(MusicType.MainMenuMusic);
    }
    
    public void ChangeVolume(float newVolume)
    {
        volume = newVolume;
        volume *= volumeK;
        _sourceAudio.Volume = volume;

    }
    public void PlayMusic(MusicType type)
    {
        _sourceAudio.Play(type.ToString());
        _sourceAudio.Loop = true;
    }
}

using System;
using System.Collections;
using NTC.Global.System;
using UnityEngine;
using Plugins.Audio.Core;

public class MusicManager : Singleton<MusicManager>
{

    
    private SourceAudio _sourceAudio;
    public float volume = 0.1f;

    [SerializeField]private float volumeK = 0.7f;
    public enum MusicType { MainMenuMusic = 0, InGameMusic };

    [SerializeField] private AudioClip[] CLips;
    [SerializeField] private float[] CLipsLength;

    private MusicType curMusicType;
    private float curTime = 0;
    void Start()
    {
        _sourceAudio = GetComponent<SourceAudio>();
        volume = (PlayerPrefs.HasKey("MUSIC_VOLUME")) ? PlayerPrefs.GetFloat("MUSIC_VOLUME") : volume;
        volume *= volumeK;

        _sourceAudio.Volume = volume;
        DontDestroyOnLoad(transform.gameObject);
        curTime = 0;
        ChangeMusic(MusicType.MainMenuMusic);
    }
    
    public void ChangeVolume(float newVolume)
    {
        volume = newVolume;
        volume *= volumeK;
        _sourceAudio.Volume = volume;

    }

    private void Update()
    {
        curTime += Time.unscaledDeltaTime;
        
        if (curTime > CLipsLength[(int)curMusicType] - 1)
        {
              _sourceAudio.Play(curMusicType.ToString());
            curTime = 0;
        }
    }

    public void ChangeMusic(MusicType type)
    {
        curTime = 0;
        curMusicType = type;
        _sourceAudio.Play(curMusicType.ToString());
    }

}

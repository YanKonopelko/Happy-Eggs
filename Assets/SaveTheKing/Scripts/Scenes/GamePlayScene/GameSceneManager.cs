using System;
using System.Collections;
using NavMeshPlus.Components;
using NTC.Global.Cache;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameSceneManager : MonoCache
{
    public static GameSceneManager instanse;
    public Action onGameStart;
    public Action<bool> onGameEnd;
    public Pet[] pet;
    private NavMeshSurface Surface2D;

    public int remainingTime = 10;
    private void Awake()
    {
        InkManager.inkAmount = 200;
        instanse = this;
        Time.timeScale = 0;
        pet = FindObjectsOfType<Pet>();
        Surface2D = FindObjectOfType<NavMeshSurface>();
        Surface2D.BuildNavMeshAsync();
        onGameEnd += Stop;
    }

    public void StartGame()
    {
        onGameStart.Invoke();
        Time.timeScale = 1;
        StartCoroutine(TimeCheck());
        StartCoroutine(Check(0.01f));
        NavUptdate();
    }
    
    private IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(1);
        remainingTime--;
        if (remainingTime == 0)
            Win();
        StartCoroutine(TimeCheck());
    }

    private void Win()
    {
        onGameEnd(true);
        PlayerPrefs.SetInt("LastLevel",PlayerPrefs.GetInt("LastLevel")+1);    

        SoundManager.Instance.PlaySound(SoundManager.SoundType.WinSound);
    }
    public void NavUptdate()
    {
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);        
    }

    public void Lose()
    {
        onGameEnd(false);
        //Reload();
        SoundManager.Instance.PlaySound(SoundManager.SoundType.LoseSound);
    }


    protected override void OnEnabled()
    {
        YandexGame.RewardVideoEvent += Skip;
    }

    protected override void OnDisabled()
    {
        YandexGame.RewardVideoEvent -= Skip;
    }

    
    public void ToNextLevel()
    {
        LevelManager.instance.NextLevel();
    }
    public void Reload()
    {
        LevelManager.instance.Reload();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
        MusicManager.Instance.ChangeMusic(MusicManager.MusicType.MainMenuMusic);

    }

    public void SkipLevel()
    {
        Debug.Log("A");
        YandexGame.Instance._RewardedShow(0);
        //YandexGame.RewVideoShow(0);
    }

    private void Skip(int a = 0)
    {
        LevelManager.instance.SkipLevel();
    }
    private void Stop(bool a)
    {
        Time.timeScale = 0;
    }
    private IEnumerator Check(float time)
    {
        yield return new WaitForSeconds(time);
        NavUptdate();
        StartCoroutine(Check(0.5f));

    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelManager : MonoBehaviour
{
    private bool isPlaying;
    public static LevelManager instance;
    private AudioSource _audioSource;

    private int AnotherScenes = 2;
    
    private uint counterToAd = 0;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else if (instance != this)
            Destroy(transform.root.gameObject);


    }

    public void NextLevel()
    {
        TryToShowAd();
        var lvl = YandexGame.savesData.LastLevel+1;
        YandexGame.savesData.LastLevel = lvl;
        YandexGame.SaveProgress();
        SceneManager.LoadScene(lvl%(SceneManager.sceneCountInBuildSettings-AnotherScenes)+AnotherScenes);
    }

    public void Reload()
    {
        TryToShowAd();

        var lvl = YandexGame.savesData.LastLevel;
        
        SceneManager.LoadScene(lvl%(SceneManager.sceneCountInBuildSettings-AnotherScenes)+AnotherScenes);
    }

    public void SkipLevel()
    {
        var lvl = YandexGame.savesData.LastLevel+1;
        
        YandexGame.savesData.LastLevel = lvl;
        YandexGame.SaveProgress();
        
        SceneManager.LoadScene(lvl%(SceneManager.sceneCountInBuildSettings-AnotherScenes)+AnotherScenes);
    }

    private void TryToShowAd()
    { 
        YandexGame.FullscreenShow();
    }
    
}

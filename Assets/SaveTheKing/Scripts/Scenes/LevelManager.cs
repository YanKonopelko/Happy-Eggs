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
        var lvl = PlayerPrefs.GetInt("LastLevel");
        PlayerPrefs.SetInt("LastLevel",lvl);    
        SceneManager.LoadScene(lvl%(SceneManager.sceneCountInBuildSettings-AnotherScenes)+AnotherScenes);
    }

    public void Reload()
    {
        TryToShowAd();
        Debug.Log(SceneManager.sceneCountInBuildSettings); 
        var lvl = PlayerPrefs.GetInt("LastLevel");
        SceneManager.LoadScene(lvl%(SceneManager.sceneCountInBuildSettings-AnotherScenes)+AnotherScenes);
    }

    public void SkipLevel()
    {
        var lvl = PlayerPrefs.GetInt("LastLevel")+1;
        PlayerPrefs.SetInt("LastLevel",lvl);    
        SceneManager.LoadScene(lvl%(SceneManager.sceneCountInBuildSettings-AnotherScenes)+AnotherScenes);
    }

    private void TryToShowAd()
    { 
        YandexGame.FullscreenShow();
    }
    
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource bgmSource;

    [Header("BGM Clips")] 
    public AudioClip mainBGM;

    public AudioClip secondSceneBGM;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ApplyBGMForScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyBGMForScene(scene.name);
    }
    
    private void ApplyBGMForScene(string sceneName)
    {
        AudioClip nextClip = mainBGM;

        if (sceneName == "SecondStage")
        {
            nextClip = secondSceneBGM;
        }

        if (bgmSource.clip != nextClip)
        {
            bgmSource.clip = nextClip;
            bgmSource.Play();
        }
    }
}

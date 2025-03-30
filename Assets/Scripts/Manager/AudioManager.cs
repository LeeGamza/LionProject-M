using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Audio Source")] 
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM Clips")] 
    public AudioClip mainBGM;
    public AudioClip secondSceneBGM;

    [Header("SFX Clips")] 
    public AudioClip pickup_machineGun;
    public AudioClip pickup_ShotGun;

    public AudioClip insertCoin;
    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip laserSound1;
    public AudioClip laserSound2;
    public AudioClip meteoDestroySound;
    public AudioClip uFODestroySound;
    public AudioClip missionStart;
    public AudioClip missionComplete;

    [Header("Boss SFX")]
    public AudioClip DeathRazerOnSound;
    public AudioClip DeathRazerSound;

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
            bgmSource.volume = 0.4f;
        }
        else if (sceneName == "FirstStage")
        {
            bgmSource.volume = 0f;
        }

        if (bgmSource.clip != nextClip)
        {
            bgmSource.clip = nextClip;
            bgmSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}

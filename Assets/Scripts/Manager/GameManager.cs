using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private HitParticles hitParticles;

    private float timer;
    private bool isTimerRun = false;
    
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
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.OnGameStart += StartGame;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGameStart -= StartGame;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (!isTimerRun) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = 0f;
            isTimerRun = false;
            Debug.Log("GameOver");
        }
    }

    private void StartGame()
    {
        hitParticles = new HitParticles();
        SceneManager.LoadScene("SecondStage");
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SecondStage")
        {
            timer = 240f;
            isTimerRun = true;
        }
        else
        {
            isTimerRun = false;
        }
    }

    public int GetTimeLeft()
    {
        return Mathf.CeilToInt(timer);
    }
}

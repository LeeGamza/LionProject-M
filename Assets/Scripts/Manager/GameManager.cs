using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private HitParticles hitParticles;
    
    private GameObject playerObj;
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
        EventManager.Instance.OnRevive += HandleOnRevive;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGameStart -= StartGame;
        EventManager.Instance.OnRevive -= HandleOnRevive;
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
            stageGameOver.Instance.TimeGameOver(); 
            Debug.Log("GameOver");
        }
    }

    private void StartGame()
    {
        hitParticles = new HitParticles();
        SceneManager.LoadScene("FirstStage");
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SecondStage")
        {
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            playerObj = spawnManager.SpawnPlayer();
            if (playerObj.TryGetComponent<Player>(out Player player))
            {
                player.GoPosition();
            }
            
            timer = 360f;
            isTimerRun = true;
        }
        else
        {
            isTimerRun = false;
        }
    }

    private void HandleOnRevive()
    {
        if (playerObj.TryGetComponent<Player>(out Player player))
        {
            if (player.CanRevive())
            {
                player.Revive();
            }
        }
    }

    public int GetTimeLeft()
    {
        return Mathf.CeilToInt(timer);
    }
}

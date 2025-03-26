using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private HitParticles hitParticles;

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
    }

    private void OnDisable()
    {
        EventManager.Instance.OnGameStart -= StartGame;
    }

    private void StartGame()
    {
        hitParticles = new HitParticles();
        SceneManager.LoadScene("SecondStage");
    }
}

using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
        //EventManager.Instance.OnGameStart += StartGame;
    }

    private void OnDisable()
    {
        //EventManager.Instance.OnGameStart -= StartGame;
    }

    private void StartGame()
    {
        GameSceneManager.Instance.LoadScene("SecondStage");
    }
}

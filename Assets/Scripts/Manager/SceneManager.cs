using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ConfigureCameraForScene(scene.name);
    }
    
    private void ConfigureCameraForScene(string sceneName)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;
        
        switch (sceneName)
        {
            case "FirstStage":
                mainCamera.aspect = 16f / 9f;
                break;
                
            case "SecondStage":
                mainCamera.aspect = 9f / 16f; 
                break;
                
            default:
                mainCamera.aspect = 16f / 9f;
                break;
        }
    }
}

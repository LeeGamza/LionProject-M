using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class stageGameOver : MonoBehaviour
{
    public GameOver_Count GameOver;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    public string gameOverSceneName = "GameOver";

    private bool isFading = false; // 코루틴 중복 실행 방지

    public static stageGameOver Instance { get; private set; }

    void Awake()
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

    void Start()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0;
        }
    }

    void Update()
    {
        if (GameOver != null && GameOver.GetisGameOver() && !isFading)
        {
            StartCoroutine(FadeOutAndLoadScene());
        }

    }

    public void TimeGameOver()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }


    private IEnumerator FadeOutAndLoadScene()
    {
        isFading = true; // 코루틴 중복 실행 방지
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            if (fadeCanvasGroup != null)
            {
                fadeCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            }
            yield return null;
        }

        SceneManager.LoadScene(gameOverSceneName);
    }
}
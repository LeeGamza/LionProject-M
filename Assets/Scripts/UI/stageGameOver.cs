using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class stageGameOver : MonoBehaviour
{
    public GameOver_Count GameOver; // Unity 에디터에서 직접 연결
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    public string gameOverSceneName = "GameOver";

    private bool isFading = false; // 코루틴 중복 실행 방지

    void Start()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0;
        }
    }

    private void Update()
    {
        if (GameOver != null && GameOver.GetisGameOver() && !isFading)
        {
            Debug.Log("GameOver 상태 감지. 페이드 아웃 시작.");
            StartCoroutine(FadeOutAndLoadScene());
        }
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

        Debug.Log("씬 전환 중...");
        SceneManager.LoadScene(gameOverSceneName);
    }
}
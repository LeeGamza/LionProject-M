using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public CanvasGroup fadeCanvas; 
    public CanvasGroup gameOverCanvas; 
    public CanvasGroup scoreCanvas;
    public RectTransform scoreCavasSize;
    public FontRender_highscore scoreFont;
    public float fadeDuration = 1.5f;

    private bool isScoreDisplayed = false; 

    void Start()
    {
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
      
        fadeCanvas.alpha = 1;
        gameOverCanvas.alpha = 0;
        scoreCanvas.alpha = 0;

        yield return StartCoroutine(FadeIn(gameOverCanvas));
        yield return StartCoroutine(FadeOut(gameOverCanvas));

        scoreCanvas.alpha = 1;
        yield return StartCoroutine(ChangeWidthOverTime(scoreCavasSize, 804f, 1f));
        yield return StartCoroutine(scoreDisplay());


        isScoreDisplayed = true;
    }

    void Update()
    {
     
        if (isScoreDisplayed && Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainScene"); 
        }
    }

    private IEnumerator FadeIn(CanvasGroup canvas)
    {
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            yield return null;
        }
        canvas.alpha = 1;
    }

    private IEnumerator FadeOut(CanvasGroup canvas)
    {
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvas.alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);
            yield return null;
        }
        canvas.alpha = 0;
    }

    private IEnumerator ChangeWidthOverTime(RectTransform rectTransform, float targetWidth, float duration)
    {
        float startWidth = rectTransform.sizeDelta.x;
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newWidth = Mathf.Lerp(startWidth, targetWidth, elapsed / duration);
            rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
            yield return null;
        }

        rectTransform.sizeDelta = new Vector2(targetWidth, rectTransform.sizeDelta.y);
    }

    private IEnumerator scoreDisplay()
    {
  
        for (int i = 0; i < scoreCanvas.transform.childCount; i++)
        {
            Transform child = scoreCanvas.transform.GetChild(i);
            child.gameObject.SetActive(false);
        }

   
        for (int i = 0; i < scoreCanvas.transform.childCount; i++)
        {
            Transform child = scoreCanvas.transform.GetChild(i);
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f); 
        }
    }

}

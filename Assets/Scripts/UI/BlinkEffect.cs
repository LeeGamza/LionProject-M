using UnityEngine;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup; 
    public float blinkInterval = 0.5f; 
   
    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            panelCanvasGroup.alpha = 0; // 즉시 사라짐
            yield return new WaitForSeconds(blinkInterval); // 대기

            panelCanvasGroup.alpha = 1; // 즉시 나타남
            yield return new WaitForSeconds(blinkInterval); // 대기
        }
    }






}

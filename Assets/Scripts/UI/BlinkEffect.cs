using UnityEngine;
using System.Collections;

public class BlinkEffect : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup; 
    public float blinkInterval = 0.5f;

    public int MAX_intervalCount = 0;
    private int intervalCount = 0;
   
    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        if (MAX_intervalCount == 0)
        {
            while (true)
            {
                panelCanvasGroup.alpha = 0;
                yield return new WaitForSeconds(blinkInterval);

                panelCanvasGroup.alpha = 1;
                yield return new WaitForSeconds(blinkInterval);
            }
        }
        else
        {
            while (intervalCount < MAX_intervalCount)
            {
                panelCanvasGroup.alpha = 0;
                yield return new WaitForSeconds(blinkInterval);

                panelCanvasGroup.alpha = 1;
                yield return new WaitForSeconds(blinkInterval);
                intervalCount++;

            }
        }

    }






}

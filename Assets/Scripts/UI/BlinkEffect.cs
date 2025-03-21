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
            panelCanvasGroup.alpha = 0; 
            yield return new WaitForSeconds(blinkInterval); 

            panelCanvasGroup.alpha = 1; 
            yield return new WaitForSeconds(blinkInterval); 
        }
    }






}

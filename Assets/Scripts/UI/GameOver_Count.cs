using UnityEngine;
using DG.Tweening;
using System.Collections;

public class GameOver_Count : MonoBehaviour
{
    public FontRender_highscore Count;
    public GameOver_dead gameOverDead;

    private int countNum = 9; 
    private bool isPlayerLive = true; 
    private bool isGameOver = false;
    private bool isCountingDown = false; // 카운트다운 중복 실행 방지

    void Start()
    {
        if (gameOverDead == null)
        {
            Debug.LogError("GameOver_dead 스크립트가 연결되지 않았습니다!");
            return;
        }

        isPlayerLive = gameOverDead.Getisplayerlive();
        Count = GetComponent<FontRender_highscore>();
    }

    void Update()
    {
        if (gameOverDead == null) return;

        isPlayerLive = gameOverDead.Getisplayerlive();

        if (Count != null && !isPlayerLive && !isCountingDown)
        {
            StartCoroutine(CountdownAndRotationRoutine()); 
        }

        if (isPlayerLive && isCountingDown)
        {
            ResetCountdown();
        }
    }

    IEnumerator CountdownAndRotationRoutine()
    {
        isCountingDown = true;
        countNum = 9; 

        while (countNum > 0)
        {
            yield return new WaitForSeconds(1f);

            yield return RotateTo(new Vector3(0, 90, 0), 0.5f);

            countNum--;
            Count.SetText(countNum.ToString());

            yield return RotateTo(new Vector3(0, 0, 0), 0.5f);

            yield return new WaitForSeconds(1f);
        }

        Count.SetText("0");
        isGameOver = true;
        isCountingDown = false;
    }

    IEnumerator RotateTo(Vector3 targetRotation, float duration)
    {
        transform.DORotate(targetRotation, duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);

        yield return new WaitForSeconds(duration);
    }

    public bool GetisGameOver()
    {
        return isGameOver;
    }

    private void ResetCountdown()
    {
        StopAllCoroutines(); 
        isCountingDown = false; 
        countNum = 9; 
        Count.SetText(countNum.ToString()); 
    }
}
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver_Count : MonoBehaviour
{
    public FontRender_highscore Count;
    public GameOver_dead gameOverDead;

    private int countNum = 9;
    private bool isPlayerLive = true;
    private bool isGameOver = false;
    private bool isCountingDown = false;

    void Start()
    {
        isPlayerLive = gameOverDead.Getisplayerlive();
        Count = GetComponent<FontRender_highscore>();
    }

    void Update()
    {
        // 플레이어 생존 상태 업데이트
        isPlayerLive = gameOverDead.Getisplayerlive();

        // Q 키를 눌렀을 때 카운트다운 초기화
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ResetCountdown();
        }

        // 플레이어가 죽었고 카운트다운이 실행 중이 아니면 시작
        if (!isPlayerLive && !isCountingDown)
        {
            StartCoroutine(CountdownAndRotationRoutine());
        }

    }

    IEnumerator CountdownAndRotationRoutine()
    {
        isCountingDown = true;

        while (countNum > 0)
        {
            if (countNum == 10)
            {
                Count.SetText("9".ToString());
            }

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
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            yield return null;
        }

        transform.rotation = endRotation;
    }

    public bool GetisGameOver()
    {
        return isGameOver;
    }

    private void ResetCountdown()
    {
        Debug.Log("ResetCountdown called."); // 디버깅: 함수 호출 확인

        // 모든 코루틴 중지
        StopAllCoroutines();

        // 상태 초기화
        isCountingDown = false;
        countNum = 10;
        isGameOver = false;

        // UI 즉시 업데이트
        Count.SetText(countNum.ToString());
    }
}
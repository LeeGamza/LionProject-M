using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver_Count : MonoBehaviour
{
    public FontRender_highscore Count;
    private GameObject player;

    private int countNum = 9;
    private bool isPlayerLive = true;
    private bool isGameOver = false;
    private bool isCountingDown = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Count = GetComponent<FontRender_highscore>();
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            isPlayerLive = true;
        }
        else
        {
            isPlayerLive = false;
        }

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
        StopAllCoroutines();

        isCountingDown = false;
        countNum = 9;
        isGameOver = false;

        Count.SetText(countNum.ToString());
    }
}
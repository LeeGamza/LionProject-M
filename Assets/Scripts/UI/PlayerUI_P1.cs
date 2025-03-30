using UnityEngine;

public class PlayerUI_P1 : MonoBehaviour
{
    private GameObject player; // Player 태그를 가진 오브젝트
    private bool isProcessing = false; // 활성화/비활성화 중인지 확인
    private bool hasActivatedOnce = false; // 처음 한 번 자동으로 작동했는지 확인

    void Start()
    {
        // 초기화 시 플레이어를 찾음
        player = GameObject.FindWithTag("Player");

        // 처음 한 번 자동으로 활성화
        if (player != null)
        {
            StartCoroutine(ActivateForOneSecond());
            hasActivatedOnce = true; // 처음 한 번 작동했음을 표시
        }
    }

    void Update()
    {
        // Q 키를 눌렀을 때 활성화/비활성화 처리 (처음 한 번 이후에만 작동)
        if (Input.GetKeyDown(KeyCode.Q) && !isProcessing && hasActivatedOnce)
        {
            StartCoroutine(ActivateForOneSecond());
        }

        // 플레이어를 따라감 (활성화된 동안만)
        if (player != null && gameObject.activeSelf)
        {
            transform.position = player.transform.position;
        }
    }

    private System.Collections.IEnumerator ActivateForOneSecond()
    {
        isProcessing = true; // 활성화/비활성화 중임을 표시

        // 자신을 활성화
        gameObject.SetActive(true);

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 자신을 비활성화
        gameObject.SetActive(false);

        isProcessing = false; // 활성화/비활성화 완료
    }
}
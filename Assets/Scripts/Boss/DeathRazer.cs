using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class DeathRazer : MonoBehaviour
{
    Transform playerTransform;
    BoxCollider2D boxCollider;

    private float duration = 2.0f; // 확장에 걸리는 시간
    private float maxHeight = 5.0f; // box collider size 최대 y 길이
    private float lifeTime = 4.0f; 

    private Vector2 originalSize; // 원래 크기 저장
    private Vector2 originalOffset; // 원래 오프셋 저장...박스콜리더를 늘릴 때 중앙에서부터 위 아래로 늘려지는걸 위에서부터 아래로만 늘려지게 조정하도록... 
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
        // 원래 크기, 위치 저장
        originalSize = boxCollider.size;
        originalOffset = boxCollider.offset;

        lifeTime = 4.0f;

        //플레이어 방향으로 레이저 회전 (0~50도, 310~360도 각도 제한) 
        InitRotating(); 
        // 생성시 자연스럽게 콜라이더를 늘리는 코루틴
        StartCoroutine(IncreaseColliderY());
        //지속시간이 끝나면 이 오브젝트를 삭제해주는 코루틴
        StartCoroutine(LifeTime());
    }
    
    // 호밍 레이저가 생성 후 collider가 자연스럽게 늘려지도록 만들어주는 코루틴
    IEnumerator IncreaseColliderY()
    {
        float elapsedTime = 0f; //경과 시간

        while (elapsedTime < duration)
        {
            //경과 시간에 따라 y 길이를 조절
            float newHeight = Mathf.Lerp(0, maxHeight, elapsedTime / duration);
            boxCollider.size = new Vector2(originalSize.x, newHeight);

            //콜라이더 offset이 위쪽에서부터 아래로 확장되도록... 다시 보니까 스프라이트 자를 때 피벗을 위쪽에 두면 이렇게 할 필요 없이 size로 조절가능할 것 같음
            boxCollider.offset = new Vector2(originalOffset.x, originalOffset.y - newHeight / 2);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }

    void InitRotating()
    {
        Vector3 dir = (playerTransform.position - this.transform.position); //보스에서 플레이어를 바라보는 방향벡터
        dir.Normalize(); //크기 1 방향벡터로 정규화

        float angle = 180.0f - Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        //Debug.Log($"초기 각도 : {angle}");
        //양 옆 각도 50도 이상 못넘어가게 angle 조건 설정
        if (angle > 50.0f && angle < 180.0f)
        {
            angle = 50.0f;
        }
        else if (angle > 180.0f && angle < 310.0f)
        {
            angle = 310.0f;
        }
        //Debug.Log($"보정 각도 : {angle}");

        transform.Rotate(Vector3.forward, angle); //Z축을 기준으로 angle만큼 회전 (플레이어를 바라보는 방향)
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) //플레이어와 충돌했다면
        {
            //플레이어 죽음 처리
            Destroy(other.gameObject); //임시로 삭제 테스트
        }
    }
}

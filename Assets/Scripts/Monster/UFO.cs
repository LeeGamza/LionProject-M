using System.Collections;
using UnityEngine;

public class UFO : Monster
{
    private Vector2 startPos;
    private Vector2 midPos;
    private Vector2 endPos;
    public float outsideOffset = 2f; // 카메라 밖 위치 오프셋

    private Camera mainCamera;

    protected override void Move()
    { 
    
    }
    protected void Move(Vector2 MidPos, Vector2 EndPos)
    {
        StartCoroutine(MoveObject(MidPos, EndPos));
    }

    protected override void Attack()
    {
        
    }

    protected override void Damaged(float damage)
    {
        
    }

    protected override void Die()
    {
        
    }

    void Start()
    {
        //  hp
        //  stack
        moveSpeed = 1.5f;

        // 생성은 스폰매니져에서?
        mainCamera = Camera.main;
        startPos = GetPosOutside();
        midPos = GetPosInside();
        endPos = GetPosOutside();

        transform.position = startPos;

        Move(midPos, endPos);
    }

    IEnumerator MoveObject(Vector2 MidPos, Vector2 EndPos)
    {
        yield return StartCoroutine(MoveObjectRoutine(MidPos));

        yield return new WaitForSeconds(1f);
        //Atack()

        yield return StartCoroutine(MoveObjectRoutine(EndPos));

        Destroy(gameObject);
    }

    IEnumerator MoveObjectRoutine(Vector2 target)
    {
        float time = 0;
        Vector2 start = transform.position;
        while (time < 1)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector2.Lerp(start, target, time);
            yield return null;
        }
    }

    Vector2 GetPosOutside()
    {
        Vector2 camPos = mainCamera.transform.position;
        float camSize = mainCamera.orthographicSize;
        float camAspect = mainCamera.aspect;

        float randomX = Random.Range(-camAspect * camSize - outsideOffset, camAspect * camSize + outsideOffset);
        float randomY = Random.Range(camSize / 2, camSize) + outsideOffset; // 절반 이상에서 생성

        return new Vector2(camPos.x + randomX, camPos.y + randomY);
    }

    Vector2 GetPosInside()
    {
        Vector2 camPos = mainCamera.transform.position;
        float camSize = mainCamera.orthographicSize;
        float camAspect = mainCamera.aspect;

        float randomX = Random.Range(-camAspect * camSize * 0.8f, camAspect * camSize * 0.8f);
        float randomY = Random.Range(-camSize * 0.1f, camSize * 0.1f);

        return new Vector2(camPos.x + randomX, camPos.y + randomY);
    }

}

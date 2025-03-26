using System.Collections;
using UnityEngine;

public class Mini_UFO : Monster
{
    public float outsideOffset = 2f; // 카메라 밖 위치 오프셋
    public GameObject bullet;
    public float attackDuration = 0.8f;
    public float waitAfterAttack = 1.5f;

    private Vector2 startPos;
    private Vector2 endPos;
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Animator ani;

    private enum State {Move, Attack}
    private State cuurentState = State.Move;
    void Start()
    {
        moveSpeed = 1.5f;

        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        StartCoroutine(StateLoop());
    }

    IEnumerator StateLoop()
    {
        while (true)
        {
            if (cuurentState == State.Move)
            {
                yield return MoveObject();
                cuurentState = State.Attack;
            }
            else if (cuurentState == State.Attack)
            {
                ani.SetBool("Move", false);
                ani.SetBool("Attack", true);
                yield return HomingAttack();
                cuurentState = State.Move;
            }
        }
    }

    IEnumerator MoveObject()
    {
        if (startPos == new Vector2(0, 0))
            startPos = GetPosOutside();
        else
            startPos = transform.position;
        endPos = GetPosInside();
        transform.position = startPos;

        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector2.Lerp(startPos, endPos, time);
            yield return null;
        }
    }

    IEnumerator HomingAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        ani.SetBool("Move", true);
        ani.SetBool("Attack", false);
        yield return new WaitForSeconds(waitAfterAttack);
    }

    public void EventFunction()
    {
        Transform pos = gameObject.GetComponent<Transform>();
        Instantiate(bullet, pos.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity);
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
        float randomY = Random.Range(-camSize * 0.1f, camSize);

        return new Vector2(camPos.x + randomX, camPos.y + randomY);
    }
}

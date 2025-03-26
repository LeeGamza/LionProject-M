using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Meteo : Monster
{
    public GameObject die;

    private Vector2 startPos;
    private Vector2 endPos;
    private Camera mainCamera;

    void Start()
    {
        moveSpeed = 4f;
        mainCamera = Camera.main;
        startPos = GetPosStart();
        endPos = GetPosEnd();

        transform.position = startPos;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);

        if ((Vector2)transform.position == endPos)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject go = Instantiate(die, gameObject.transform.position, Quaternion.identity);
        Destroy(go, 1f);
    }

    /*플레이어와 상호작용
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //데미지 주고
            Destroy(gameObject);
        }
    }
    */

    Vector2 GetPosStart()
    {
        float camSize = mainCamera.orthographicSize;
        float camAspect = mainCamera.aspect;

        float randomX = Random.Range(-camAspect * camSize - 1f, camAspect * camSize + 1f);
        float randomY = mainCamera.transform.position.y + camSize + 1f;

        return new Vector2(randomX, randomY);
    }

    Vector2 GetPosEnd()
    {
        float camSize = mainCamera.orthographicSize;
        float camAspect = mainCamera.aspect;

        float randomX = Random.Range(-camAspect * camSize -1f, camAspect * camSize + 1f);
        float randomY = mainCamera.transform.position.y - camSize - 1f;

        return new Vector2(randomX, randomY);
    }
}

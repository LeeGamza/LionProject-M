using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Meteo : Monster
{
    private Vector2 startPos;
    private Vector2 endPos;
    private Camera mainCamera;

    private bool preventOnDestroy = false;

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
            CustomDestroy();
            //Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (preventOnDestroy) return;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.meteoDestroySound);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            if (collision.GetComponentInParent<Player>() is { } player)
            {
                player.TakeDamage();
            }

            GameObject dieAnim = Instantiate(die, gameObject.transform.position, Quaternion.identity);
            Destroy(dieAnim, 1f);

            Destroy(gameObject);
        }
    }



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

    public void CustomDestroy()
    {
        preventOnDestroy = true;
        Destroy(gameObject);
    }
}

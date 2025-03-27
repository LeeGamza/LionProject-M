using System.Collections;
using UnityEngine;

public class UFO : Monster
{
    public float outsideOffset = 2f; // 카메라 밖 위치 오프셋

    private Vector2 startPos;
    private Vector2 midPos;
    private Vector2 endPos;
    private Camera mainCamera;

    private bool preventOnDestroy = false;

    protected override void Move()
    {
        StartCoroutine(MoveObject(midPos, endPos));
    }

    void Start()
    {
        moveSpeed = 1.5f;

        mainCamera = Camera.main;
        startPos = GetPosOutside();
        midPos = GetPosInside();
        endPos = GetPosOutside();

        transform.position = startPos;

        Move();
    }

    private void OnDestroy()
    {
        if (preventOnDestroy) return;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.UFODestroySound);
    }



    IEnumerator MoveObject(Vector2 MidPos, Vector2 EndPos)
    {
        yield return MoveObjectRoutine(MidPos);

        ToggleChildObject("UFO_Attack", true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.laserSound2);
        yield return new WaitForSeconds(0.667f);
        

        ToggleChildObject("UFO_Attack", false);        

        yield return MoveObjectRoutine(EndPos);

        CustomDestroy();
        //Destroy(gameObject);
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

    void ToggleChildObject(string childName, bool state)
    {
        Transform childPrefab = FindChildByName(gameObject, childName);

        if (childPrefab != null)
        {
            childPrefab.gameObject.SetActive(state); // 상태 전환
        }
    }

    private Transform FindChildByName(GameObject parent, string childName)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.name == childName)
            {
                return child;
            }
        }
        return null;
    }

    public void CustomDestroy()
    {
        preventOnDestroy = true;
        Destroy(gameObject);
    }
}

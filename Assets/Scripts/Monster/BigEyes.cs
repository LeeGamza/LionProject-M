using UnityEngine;
using DG.Tweening;

public class BigEyes : MonoBehaviour
{
    public GameObject[] BigEye;
    public Ease Ease;
    public float followDelay = 0.15f;

    private Camera mainCamera;
    private Vector3 rndPos;
    private Vector3 startPos;
    private Vector3 midPos;
    private Vector3 endPos;
    private Vector3 addPos1;
    private Vector3 addPos2;

    void Start()
    {
        addPos1 = new Vector3(12f, -1f, 0);
        addPos2 = new Vector3(0, -2f, 0);

        //rndPos = GetStartPos();
        

        for (int i = 0; i < BigEye.Length; i++)
        {
            Transform transform = BigEye[i].transform;
            //transform.position = rndPos;
            if (i == 0)
            {
                BigEye[i].GetComponent<Renderer>().material.DOColor(Color.red, 0);

                startPos = transform.position;
                midPos = transform.position + addPos1;
                endPos = transform.position + addPos2;
                Vector3[] path = { startPos, midPos, endPos };

                transform.DOPath(path, 3f, PathType.CatmullRom)
                    .SetEase(Ease)
                    .SetAutoKill(true)
                    .OnComplete(() => Destroy(BigEye[i].gameObject));
            }
            else
            {
                Transform target = BigEye[i - 1].transform;

                startPos = target.position;
                midPos = target.position + addPos1;
                endPos = target.position + addPos2;
                Vector3[] path = { startPos, midPos, endPos };

                transform.DOPath(path, 3f, PathType.CatmullRom)
                    .SetEase(Ease)
                    .SetDelay(followDelay)
                    .SetAutoKill(true)
                    .OnComplete(() => Destroy(BigEye[i].gameObject));

                followDelay += 0.15f;
            }
        }
    }


    Vector3 GetStartPos()
    {
        Vector3 camPos = mainCamera.transform.position;
        float camSize = mainCamera.orthographicSize;
        float camAspect = mainCamera.aspect;
        
        float randomX, randomY;

        int randomValue = Random.Range(0, 2);
        if (randomValue == 0)
            randomX = -camAspect * camSize - 1f;
        else
            randomX = camAspect * camSize + 1f;


        randomY = Random.Range(camPos.y + camSize / 3, camPos.y + camSize);
        Debug.Log($"{randomX}, {randomY}");
        return new Vector3(randomX, randomY, 0);
    }
}
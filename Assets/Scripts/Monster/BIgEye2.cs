using DG.Tweening;
using UnityEngine;

public class BIgEye2 : MonoBehaviour
{
    public Transform target;
    public Ease Ease1;
    public float followDelay = 0.5f;


    private Vector3 startPos;
    private Vector3 midPos;
    private Vector3 endPos;

    void Start()
    {
        // 몬스터1의 경로 따라감
        startPos = target.position;
        midPos = target.position + new Vector3(12f, -1f, 0);
        endPos = target.position + new Vector3(0, -2f, 0);
        Vector3[] path = { startPos, midPos, endPos };

        transform.DOPath(path, 3f, PathType.CatmullRom)
            .SetEase(Ease1)
            .SetDelay(followDelay)
            .SetAutoKill(true)
            .OnComplete(() => Destroy(gameObject));
    }
}

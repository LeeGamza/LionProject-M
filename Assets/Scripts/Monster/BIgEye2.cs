using DG.Tweening;
using UnityEngine;

public class BIgEye2 : MonoBehaviour
{
    public Transform target;
    public Ease Ease1;
    public float followDelay = 0.0f;

    private Vector3 startPos;
    private Vector3 midPos;
    private Vector3 endPos;
    private Vector3 addPos1;
    private Vector3 addPos2;

    void Start()
    {
        addPos1 = new Vector3(12f, -1f, 0);
        addPos2 = new Vector3(0, -2f, 0);

        startPos = target.position;
        midPos = target.position + addPos1;
        endPos = target.position + addPos2;
        Vector3[] path = { startPos, midPos, endPos };

        transform.DOPath(path, 3f, PathType.CatmullRom)
            .SetEase(Ease1)
            .SetDelay(followDelay)
            .SetAutoKill(true)
            .OnComplete(() => Destroy(gameObject));
    }
}

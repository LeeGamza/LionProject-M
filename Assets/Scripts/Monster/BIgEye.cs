using UnityEngine;
using DG.Tweening;

public class BigEye : MonoBehaviour
{
    public Ease Ease1;
    private Vector3 startPos;
    private Vector3 midPos;
    private Vector3 endPos;
    private Vector3 addPos1;
    private Vector3 addPos2;

    void Start()
    {
        addPos1 = new Vector3(12f, -1f, 0);
        addPos2 = new Vector3(0, -2f, 0);

        transform.GetComponent<Renderer>().material.DOColor(Color.red, 0);

        startPos = transform.position;
        midPos = transform.position + addPos1;
        endPos = transform.position + addPos2;
        Vector3[] path = { startPos, midPos, endPos };

        transform.DOPath(path, 3f, PathType.CatmullRom)
            .SetEase(Ease1)
            .SetAutoKill(true)
            .OnComplete(() => Destroy(gameObject));
    }
}

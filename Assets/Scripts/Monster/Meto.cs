using Unity.VisualScripting;
using UnityEngine;

public class Meto : Monster
{
    private Vector2 startPos;
    private Vector2 endPos;
    private Camera mainCamera;

    void Start()
    {
        moveSpeed = 5f;
    }

    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }
}

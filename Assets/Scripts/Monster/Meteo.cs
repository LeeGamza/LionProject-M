using UnityEngine;

public class Meteo : MonoBehaviour
{
    public float speed = 2.0f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

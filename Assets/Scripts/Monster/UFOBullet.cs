using UnityEngine;

public class UFOBullet : MonoBehaviour
{
    public float speed = 7f;
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

    /*플레이어와 상호작용
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
    */
}

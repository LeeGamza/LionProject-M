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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            //player.Damaged(attack);
            Destroy(gameObject);
        }
    }
}

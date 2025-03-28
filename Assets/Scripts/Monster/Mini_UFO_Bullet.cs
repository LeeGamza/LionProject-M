using UnityEngine;

public class Mini_UFO_Bullet : MonoBehaviour
{
    public GameObject target;
    public float speed = 7f;
    private Vector2 dir;
    private Vector2 dirNo;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        dir = target.transform.position - transform.position;
        dirNo = dir.normalized;
    }

    void Update()
    {
        if (dirNo.y == 0) dirNo.y = 0.01f; // DevideByZero 방지

        float angle = Mathf.Atan(dirNo.x / dirNo.y) * Mathf.Rad2Deg * -1;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
            if (collision.GetComponentInParent<Player>() is { } player)
            {
                player.TakeDamage();
            }
            Destroy(gameObject);
        }
    }

}


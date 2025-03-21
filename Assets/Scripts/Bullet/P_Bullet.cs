using UnityEngine;

public class P_Bullet : MonoBehaviour
{
    public float speed = 4.5f;
    

    // Update is called once per frame
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime,Space.World);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            //몬스터
            Destroy(collision.gameObject);
            
            //미사일 삭제
            Destroy(gameObject);

        }
    }
}

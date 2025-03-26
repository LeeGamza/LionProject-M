using UnityEngine;

public class P_Bullet : MonoBehaviour
{
    public float speed = 30f;
    private float _damage = 50;

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
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Monster")
        {
            Debug.Log("몬스터레이어 들어옴");
            Monster monster = collision.GetComponent<Monster>();

            if (monster != null)
            {
                Debug.Log("몬스터 컴포넌트 찾음, 데미지 적용: " + _damage);
                monster.Damaged(_damage);
            }
            
            Destroy(gameObject);

        }
    }
}

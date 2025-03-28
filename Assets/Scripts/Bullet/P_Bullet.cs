using System;
using UnityEngine;

public class P_Bullet : MonoBehaviour
{
    private bool canReturn = false;
    
    public float speed = 30f;
    private float _damage = 50;

    private void OnEnable()
    {
        canReturn = false;
        Invoke(nameof(CanReturn), 0.2f);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime,Space.World);
    }

    private void CanReturn()
    {
        canReturn = true;
    }
    private void OnBecameInvisible()
    {
        if(canReturn)
            PoolManager.Instance.Return(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Monster")
        {
            Monster monster = collision.GetComponent<Monster>();

            if (monster != null)
            {
                Debug.Log("몬스터 컴포넌트 찾음, 데미지 적용: " + _damage);
                monster.Damaged(_damage);
            }
            
            PoolManager.Instance.Return(gameObject);
        }


        if (LayerMask.LayerToName(collision.gameObject.layer) == "Boss")
        {
            BossBehaviour boss = collision.GetComponent<BossBehaviour>();

            if (boss != null)
            {
                Debug.Log("보스 컴포넌트 찾음, 데미지 적용: " + _damage);
                boss.Damaged(_damage);
            }

            PoolManager.Instance.Return(gameObject);
        }
    }
}

using UnityEngine;

//부모클래스 임시설정
public class Monster : MonoBehaviour
{
    protected float hp = 100f;
    protected float attack = 1f;
    protected float moveSpeed = 1f;

    protected virtual void Move()
    { }

    protected virtual void Attack()
    { }

    protected virtual void Damaged(float damage)
    {
        hp -= damage;

        if (hp <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}

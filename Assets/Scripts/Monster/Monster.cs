using UnityEngine;

//부모클래스 임시설정
public class Monster : MonoBehaviour
{
    public float hp = 100f;
    public float attack = 1f;
    public float moveSpeed = 1f;

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

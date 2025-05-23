using System;
using UnityEngine;

//부모클래스 임시설정
public class Monster : MonoBehaviour
{
    public GameObject die;

    private SpriteRenderer spriteRenderer;
    private Color hitColor = Color.red;
    
    protected float hp = 100f;
    protected float attack = 1f;
    protected float moveSpeed = 1f;

    private void Awake()
    {
        if (!TryGetComponent<SpriteRenderer>(out spriteRenderer))
        {
            Debug.LogError("Monster : UnFound SpriteRenderer", this);
        }
    }

    protected virtual void Move()
    { }

    protected virtual void Attack()
    { }

    public virtual void Damaged(float damage)
    {
        Debug.Log("몬스터가 데미지를 받음: " + damage + ", 현재 HP: " + hp);
        hp -= damage;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSound);
        EventManager.Instance.InvokeHitEffect(spriteRenderer, hitColor);

        if (hp <= 0)
        {
            GameObject dieAnim = Instantiate(die, gameObject.transform.position, Quaternion.identity);
            Destroy(dieAnim, 1f);

            Die();
        }
            
    }

    protected virtual void Die()
    {
        EventManager.Instance.InvokeDrop(transform.position);
        Destroy(gameObject);
    }

}

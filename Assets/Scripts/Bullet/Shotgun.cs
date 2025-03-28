using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private bool canReturn = false;
    private float _damage = 50;
    
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
    }
    
    public void OnAnimationEnd()
    {
        PoolManager.Instance.Return(gameObject);
    }
}

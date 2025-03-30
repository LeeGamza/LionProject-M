using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private float _damage = 50f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Monster")
        {
            if (collision.TryGetComponent(out Monster monster))
            {
                monster.Damaged(_damage);
            }
        }

        if (LayerMask.LayerToName(collision.gameObject.layer) == "Boss")
        {
            BossBehaviour boss = collision.GetComponent<BossBehaviour>();

            if (boss != null)
            {
                boss.Damaged(_damage);
            }

            PoolManager.Instance.Return(gameObject);
        }

    }

    public void OnAnimationEnd()
    {
        PoolManager.Instance.Return(gameObject);
    }
}

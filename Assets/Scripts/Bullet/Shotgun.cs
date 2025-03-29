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
    }

    public void OnAnimationEnd()
    {
        PoolManager.Instance.Return(gameObject);
    }
}

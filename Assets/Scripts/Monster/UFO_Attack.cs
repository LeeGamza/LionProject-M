using UnityEngine;

public class UFO_Attack : MonoBehaviour
{
    public GameObject bullet;
    void FireBullet()
    {
        Transform pos = gameObject.GetComponent<Transform>();
        Instantiate(bullet, pos.position + new Vector3(0, -1f, 0), Quaternion.identity);
    }
}

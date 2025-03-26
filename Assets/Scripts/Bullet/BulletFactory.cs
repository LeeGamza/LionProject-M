using UnityEngine;
using Bullet;

public class BulletFactory
{
    private static GameObject[] _bullet;

    public static void SetBullet(GameObject[] prefabs)
    {
        _bullet = prefabs;
    }

    public static GameObject CreateBullet(BulletType type, Vector3 pos, Quaternion rotation)
    {
        if (_bullet == null)
        {
            Debug.LogError("BulletFactory : bulletPrefab is null");
            return null;
        }

        int index = (int)type;
        
        if (index < 0 || index >= _bullet.Length)
        {
            Debug.LogError("BulletFactory : bulletType Error");
            return null;
        }

        var prefab = _bullet[index];
        return PoolManager.Instance.Spawn(prefab, pos, rotation);
    }
}
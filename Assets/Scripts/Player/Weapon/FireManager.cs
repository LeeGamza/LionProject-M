using System.Collections.Generic;
using UnityEngine;
using Bullet;

public class FireManager 
{
    private Transform[] muzzles;
    private GameObject[] effectPrefabs;
    private BulletType bulletType = BulletType.Basic;
    private int curruntammo;
    public BulletType CurrentBulletType => bulletType;

    public void Fire()
    {
        for (int i = 0; i < muzzles.Length; i++)
        {
            if (muzzles[i] != null && muzzles[i].gameObject.activeInHierarchy)
            {
                Quaternion bulletRotation = Quaternion.Euler(0f, 0f, 90f);
                if (bulletType == BulletType.Shotgun)
                {
                    BulletFactory.CreateBullet(bulletType, muzzles[i].position + Vector3.up * 2f, bulletRotation);
                }
                else
                {
                    BulletFactory.CreateBullet(bulletType, muzzles[i].position, bulletRotation);
                }
                curruntammo--;
                Debug.Log(curruntammo);

                if (effectPrefabs != null && effectPrefabs.Length > 0)
                {
                    if (i == 0)
                    {
                        Object.Instantiate(effectPrefabs[0], muzzles[i].position + Vector3.up * 0.5f, Quaternion.identity);
                    }
                    else
                    {
                        Object.Instantiate(effectPrefabs[1], muzzles[i].position + Vector3.up * 0.5f, Quaternion.identity);
                    }
                }
            }
        }
    }

    public void SetBulletType(BulletType type)
    {
        bulletType = type;
        curruntammo = type.GetBulletNum();
    }
    
    public void SetMuzzle(Transform singleMuzzle)
    {
        muzzles = new[] { singleMuzzle };
    }

    public void UpgradeMuzzles(Transform[] addMuzzles)
    {
        if (muzzles != null && muzzles.Length > 1)
            return;

        if (muzzles == null)
        {
            muzzles = addMuzzles;
            return;
        }
        var combined = new List<Transform>(muzzles);
        combined.AddRange(addMuzzles);
        muzzles = combined.ToArray();
    }
    
    public void SetEffectPrefabs(GameObject[] newEffectPrefabs)
    {
        effectPrefabs = newEffectPrefabs;
    }

    public int Curruntammo()
    {
        return curruntammo;
    }
}
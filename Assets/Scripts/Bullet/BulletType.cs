using UnityEngine;

namespace Bullet
{
    public enum BulletType 
    {
        Basic,
        MachinGun,
    }

    public static class BulletNum
    {
        public static int GetBulletNum(this BulletType bulletType)
        {
            return bulletType switch
            {
                BulletType.Basic => -1,         
                BulletType.MachinGun => 200
            };
        }
    }
}

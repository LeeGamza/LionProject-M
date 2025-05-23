using UnityEngine;

namespace Bullet
{
    public enum BulletType 
    {
        Basic,
        MachinGun,
        Shotgun,
        Rocket
    }

    public static class BulletNum
    {
        public static int GetBulletNum(this BulletType bulletType)
        {
            return bulletType switch
            {
                BulletType.Basic => -1,         
                BulletType.MachinGun => 200,
                BulletType.Shotgun => 30,
                BulletType.Rocket => 10,
            };
        }
    }
}

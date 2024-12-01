using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPenetration : ITreeBulletUpgrade
{
    [SerializeField] private int additionalCount;

    public override void UpgradeBullet(Bullet bullet)
    {
        bullet.Penetration += additionalCount;
    }
}

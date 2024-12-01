using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCrit : ITreeBulletUpgrade
{
    [SerializeField] float additionalChance;

    public override void UpgradeBullet(Bullet bullet)
    {
        bullet.CritChance += additionalChance;
    }
}

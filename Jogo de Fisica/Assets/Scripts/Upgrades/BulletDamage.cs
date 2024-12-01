using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : ITreeBulletUpgrade
{
    [SerializeField] int additionalDamage;

    public override void UpgradeBullet(Bullet bullet)
    {
        bullet.Damage += additionalDamage;
    }
}

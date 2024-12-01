using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKnockback : ITreeBulletUpgrade
{
    [SerializeField] float force;

    public override void UpgradeBullet(Bullet bullet)
    {
        bullet.Knockback += force;
    }
}

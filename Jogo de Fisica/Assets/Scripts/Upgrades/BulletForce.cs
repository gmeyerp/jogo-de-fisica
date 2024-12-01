using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForce : ITreeBulletUpgrade
{
    [SerializeField] float amount;
    public override void UpgradeBullet(Bullet bullet)
    {
        bullet.InitialForce += amount;
    }
}

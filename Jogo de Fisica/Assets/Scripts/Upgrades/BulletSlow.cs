using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSlow : ITreeBulletUpgrade
{
    [SerializeField] private float amount;
    [SerializeField] private float duration;

    public override void UpgradeBullet(Bullet bullet)
    {
        Debug.Log(bullet.Slow);
        bullet.Slow = (
            bullet.Slow.Amount + amount,
            bullet.Slow.Duration + duration
        );
    }
}

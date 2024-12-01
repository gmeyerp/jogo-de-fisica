using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootExplode : ITreeShootUpgrade
{
    [SerializeField] float radius;
    [SerializeField] GameObject vfx;

    public override void Shoot(Bullet prefab, Vector3 position, Vector3 direction, HashSet<ITreeBulletUpgrade> upgrades, Turret turret)
    {
        Bullet bullet = Bullet.Shoot(prefab, position, direction, upgrades);
        bullet.Explode(radius, vfx);
    }
}

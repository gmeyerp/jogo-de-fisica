using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpread : ITreeShootUpgrade
{
    [SerializeField] int count;
    [SerializeField] float angle;

    public override void Shoot(Bullet prefab, Vector3 position, Vector3 direction, HashSet<ITreeBulletUpgrade> upgrades, Turret turret)
    {
        float angleStart = -angle / 2;
        float angleEnd = angle / 2;
        float angleStep = angle / count;

        for (float currentAngle = angleStart; currentAngle < angleEnd; currentAngle += angleStep)
        {
            Vector3 currentDirection = Quaternion.AngleAxis(currentAngle, Vector3.up) * direction;
            Bullet.Shoot(prefab, position, currentDirection, upgrades);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : ShootStyle
{
    public override void Shoot(Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower)
    {
        Bullet bullet = Instantiate(prefab, spawnPosition, spawnRotation);
        bullet.Shoot(direction * weaponPower);
    }
}

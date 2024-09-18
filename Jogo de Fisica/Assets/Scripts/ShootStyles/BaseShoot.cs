using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShoot : ShootStyle
{
    public override void Shoot(Vector3 direction, Bullet bulletPrefab, Vector3 position, Quaternion rotation, float weaponPower)
    {
        Bullet bullet = Instantiate(prefab, position, rotation);
        bullet.Shoot(direction * weaponPower);
    }
}

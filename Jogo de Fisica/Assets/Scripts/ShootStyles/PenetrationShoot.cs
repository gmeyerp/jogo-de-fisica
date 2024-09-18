using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetrationShoot : ShootStyle
{
    
    public override void Shoot(Vector3 direction, Bullet bulletPrefab, Vector3 position, Quaternion rotation, float weaponPower)
    {
        Bullet bullet = Instantiate(prefab, position, rotation);
        bullet.PenetrationOn();
        Debug.Log("Penetration");
        bullet.Shoot(direction * weaponPower);
    }
}

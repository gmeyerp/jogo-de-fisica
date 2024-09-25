using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PenetrationShootStyle", menuName = "Scriptable Objects/Shoot Styles/Penetration", order = 1)]
public class PenetrationShoot : ShootStyle
{  
    public override void Shoot(Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower)
    {
        Bullet bullet = Instantiate(prefab, spawnPosition, spawnRotation);
        bullet.PenetrationOn();
        Debug.Log("Penetration");
        bullet.Shoot(direction * weaponPower);
    }
}

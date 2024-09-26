using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlowShootStyle", menuName = "Scriptable Objects/Shoot Styles/Slow", order = 3)]
public class SlowShoot : ShootStyle
{
    public override void Shoot(Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower)
    {
        Bullet bullet = Instantiate(prefab, spawnPosition, spawnRotation);
        bullet.Shoot(direction * weaponPower);
        bullet.SlowOn();
    }
}

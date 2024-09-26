using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleShootStyle", menuName = "Scriptable Objects/Shoot Styles/Double", order = 4)]
public class DoubleShoot : ShootStyle
{
    [SerializeField] float delay;
    public override void Shoot(Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower)
    {
        Bullet bullet = Instantiate(prefab, spawnPosition, spawnRotation);
        bullet.Shoot(direction * weaponPower);
        bullet.ShootBaseBullet(delay, direction, spawnPosition, spawnRotation, weaponPower, prefab);
    }
}

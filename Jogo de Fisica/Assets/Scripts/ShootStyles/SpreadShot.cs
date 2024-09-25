using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpreadShootStyle", menuName = "Scriptable Objects/Shoot Styles/Spread", order = 2)]
public class SpreadShot : ShootStyle
{
    [SerializeField] float angle = 30f;
    public override void Shoot(Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower)
    {        
        Bullet bullet = Instantiate(prefab, spawnPosition, spawnRotation);
        bullet.Shoot(direction * weaponPower);

        Bullet bullet1 = Instantiate(prefab, spawnPosition, spawnRotation);
        Vector3 left = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
        bullet1.Shoot(left * weaponPower);

        Vector3 right = Quaternion.AngleAxis(angle, Vector3.up) * direction;
        Bullet bullet2 = Instantiate(prefab, spawnPosition, spawnRotation);
        bullet2.Shoot(right * weaponPower);
    }
}

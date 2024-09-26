using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaDamageStyle", menuName = "Scriptable Objects/Shoot Styles/AreaDamage", order = 5)]
public class AreaDamageShoot : ShootStyle
{
    [SerializeField] float area;
    [SerializeField] GameObject vfx; // ajustar manualmente o vfx ate fazer script dele, se necessario
    public override void Shoot(Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower)
    {
        Bullet bullet = Instantiate(prefab, spawnPosition, spawnRotation);
        bullet.Explode(area, vfx);
    }
}

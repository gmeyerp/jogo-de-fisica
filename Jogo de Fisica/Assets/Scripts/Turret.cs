using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private TurretArea area;

    [Header("Weapon")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float weaponPower = 10f;
    [SerializeField] private float weaponCooldown = 2f;
    private float weaponCooldownLeft;

    private void Update()
    {
        if (weaponCooldownLeft > 0)
        {
            if (weaponCooldownLeft > Time.deltaTime)
            { weaponCooldownLeft -= Time.deltaTime; }
            else
            { weaponCooldownLeft = 0; }
        }

        if (area.TargetCount > 0)
        {
            Vector3 target = area.First.transform.position;
            Vector3 direction = Vector3.Normalize(target - transform.position);

            // TODO: suavizar a rotação
            transform.rotation = Quaternion.LookRotation(direction);

            if (weaponCooldownLeft == 0)
            {
                Shoot(direction);
                weaponCooldownLeft = weaponCooldown;
            }
        }
    }

    private void Shoot(Vector3 direction)
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        bullet.Shoot(direction * weaponPower);
    }
}

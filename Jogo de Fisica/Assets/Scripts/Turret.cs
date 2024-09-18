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

    [Header("Upgrades")]
    public bool[] upgrades = new bool[4];
    public int[] costs = {5,10,10,5};
    public bool damageIncrease;
    [SerializeField] int damageIncreaseCost = 5;
    public bool penetrationShot;
    [SerializeField] int penetrationShotCost = 10;
    public bool rangeIncrease;
    [SerializeField] int rangeIncreaseCost = 10;
    [SerializeField] float rangeIncreaseAmount = 1.2f;
    public bool fireSpeed;
    [SerializeField] float fireSpeedReduction = 1.2f;
    [SerializeField] int fireSpeedIncreaseCost = 5;


    private void Update()
    {
        if (weaponCooldownLeft > Time.deltaTime)
        { weaponCooldownLeft -= Time.deltaTime; }
        else
        { weaponCooldownLeft = 0; }

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
        if (upgrades[0])
        { bullet.DamageIncrease(); }
        if (upgrades[1])
        { bullet.PenetrationOn(); }
        bullet.Shoot(direction * weaponPower);
    }

    public void UpgradeDamageIncrease()
    {
        upgrades[0] = true;
    }

    public void UpgradePenetrationShot()
    {
        upgrades[1] = true;
    }

    public void UpgradeRangeIncrease()
    {
        upgrades[2] = true;
        area.gameObject.GetComponent<SphereCollider>().radius *= rangeIncreaseAmount;
    }

    public void UpgradeFireSpeed()
    {
        upgrades[3] = true;
        weaponCooldown /= fireSpeedReduction;
    }
}

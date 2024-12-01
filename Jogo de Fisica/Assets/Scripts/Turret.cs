using System;
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
    [SerializeField] List<int> teste;
    [SerializeField] List<ShootStyle> shootPattern;
    [SerializeField] Item secondShot;
    [SerializeField] Item thirdShot;
    [SerializeField] ShootCircularList shootList;

    [Header("Upgrades")]
    public bool[] upgrades = new bool[4];
    public int[] costs = { 5, 10, 10, 5 };
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

    [Header("Skill Tree")]
    public bool canMove = false;
    public ITreeShootUpgrade shootUpgrade;
    public HashSet<ITreeBulletUpgrade> bulletUpgrades;
    public HashSet<ITreeUpgrade> boughtUpgrades;

    public void Start()
    {
        shootUpgrade = new ITreeShootUpgrade.BaseUpgrade();
        bulletUpgrades = new HashSet<ITreeBulletUpgrade>();
        boughtUpgrades = new HashSet<ITreeUpgrade>();

        //deve ser possivel resolver isso usando Scriptable Object para armazenar o setup inicial das turrets e evitar essa lista nativa
        foreach (ShootStyle shootStyle in shootPattern) //fiz essa solucao feia pra ficar mais facil montar o estado inicial das turrets pelo editor
        {
            try
            {
                shootList.Add(shootStyle);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }


    private void Update()
    {
        if (weaponCooldownLeft > Time.deltaTime)
        { weaponCooldownLeft -= Time.deltaTime; }
        else
        { weaponCooldownLeft = 0; }

        if (area.TargetCount > 0)
        {
            Vector3 target = area.First.transform.position;
            Vector3 direction = Vector3.Normalize(target + (Vector3.up * bulletSpawnPoint.position.y) - transform.position);

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
        shootUpgrade.Shoot(bulletPrefab, bulletSpawnPoint.position, direction, bulletUpgrades, this);

        //shootPattern[shootCounter].Shoot(direction, bulletSpawnPoint.position, bulletSpawnPoint.rotation, weaponPower);
        //shootCounter++;
        //shootList.Shoot(direction, bulletSpawnPoint.position, bulletSpawnPoint.rotation, weaponPower);
        //secondShot = shootList.first.next;
        //thirdShot = secondShot.next;
        //if (shootCounter >= shootPattern.Count)
        //{
        //    shootCounter = 0;
        //}
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

    public ShootStyle GetShootStyle(int index)
    {
        return shootList.ElementAt(index);
    }
    public bool SetShootStyle(int index, ShootStyle style)
    {
        return shootList.ReplaceAt(style, index);
    }

    public float Cooldown { get => weaponCooldown; set => weaponCooldown = value; }
}

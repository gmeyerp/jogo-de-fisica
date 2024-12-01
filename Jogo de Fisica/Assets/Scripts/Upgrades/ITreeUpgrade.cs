using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ITreeUpgrade : MonoBehaviour
{
    public virtual void UpgradeTurret(Turret turret)
    {
        Debug.Log("Upgrade: " + turret.gameObject.name);
        UpgradeCanvas.instance.targetTurret.boughtUpgrades.Add(this);
    }
}

public abstract class ITreeShootUpgrade : ITreeUpgrade
{
    public override void UpgradeTurret(Turret turret)
    {
        base.UpgradeTurret(turret);
        turret.shootUpgrade = this;
    }

    public abstract void Shoot(Bullet prefab, Vector3 position, Vector3 direction, HashSet<ITreeBulletUpgrade> upgrades, Turret turret);

    public class BaseUpgrade : ITreeShootUpgrade
    {
        public override void Shoot(Bullet prefab, Vector3 position, Vector3 direction, HashSet<ITreeBulletUpgrade> upgrades, Turret turret)
        {
            Bullet.Shoot(prefab, position, direction, upgrades);
        }
    }
}

public abstract class ITreeBulletUpgrade : ITreeUpgrade
{
    public override void UpgradeTurret(Turret turret)
    {
        base.UpgradeTurret(turret);
        turret.bulletUpgrades.Add(this);
    }

    public abstract void UpgradeBullet(Bullet bullet);
}

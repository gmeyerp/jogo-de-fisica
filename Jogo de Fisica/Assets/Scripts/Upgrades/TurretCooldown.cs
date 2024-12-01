using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCooldown : ITreeUpgrade
{
    [SerializeField] float reduceBy;
    public override void UpgradeTurret(Turret turret)
    {
        base.UpgradeTurret(turret);
        turret.Cooldown = Mathf.Max(0, turret.Cooldown - reduceBy);
    }
}

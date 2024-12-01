using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMove : ITreeUpgrade
{
    public override void UpgradeTurret(Turret turret)
    {
        base.UpgradeTurret(turret);
        turret.canMove = true;
    }
}

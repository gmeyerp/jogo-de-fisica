using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ShootStyle : MonoBehaviour
{
    [SerializeField] protected Bullet prefab;
    public abstract void Shoot(Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower);
}

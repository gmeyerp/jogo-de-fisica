using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShootStyle : ScriptableObject
{
    [SerializeField] protected Bullet prefab;
    [SerializeField] private Image image;
    public abstract void Shoot(Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower);
}

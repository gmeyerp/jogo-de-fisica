using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootStyle : ScriptableObject
{
    [SerializeField] protected Bullet prefab;
    [SerializeField] private Sprite sprite;
    public abstract void Shoot(Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower);

    public Sprite Sprite => sprite;
}

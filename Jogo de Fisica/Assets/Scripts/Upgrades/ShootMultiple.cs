using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMultiple : ITreeShootUpgrade
{
    [SerializeField] int count;
    [SerializeField] float delay;

    public override void Shoot(Bullet prefab, Vector3 position, Vector3 direction, HashSet<ITreeBulletUpgrade> upgrades, Turret turret)
    {
        turret.StartCoroutine(Coroutine_Shoot(prefab, position, direction, upgrades));
    }

    private IEnumerator Coroutine_Shoot(Bullet prefab, Vector3 position, Vector3 direction, HashSet<ITreeBulletUpgrade> upgrades)
    {
        for (int i = 0; i < count; i++)
        {
            Bullet.Shoot(prefab, position, direction, upgrades);
            yield return new WaitForSeconds(delay);
        }
    }
}

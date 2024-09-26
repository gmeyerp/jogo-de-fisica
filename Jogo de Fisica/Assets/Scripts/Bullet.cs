using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] int bulletDamage = 1;
    [SerializeField] bool penetration;
    [SerializeField] bool isSlow;
    [SerializeField] float slowDuration;
    [SerializeField] float slowAmount;

    [SerializeField] private float lifeSpan = 5f;
    private float lifeSpanLeft;
    private void Awake()
    {
        lifeSpanLeft = lifeSpan;
    }

    private void Update()
    {
        if (lifeSpanLeft > Time.deltaTime)
        { lifeSpanLeft -= Time.deltaTime; }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(bulletDamage);
            }
            if (!penetration)
            {
                Destroy(gameObject);
            }
            if (isSlow)
            {
                enemy.SlowDown(slowAmount, slowDuration);
            }
        }
    }

    public void Shoot(Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.VelocityChange);
        lifeSpan = lifeSpanLeft;
    }

    public void PenetrationOn()
    {
        penetration = true;
    }

    public void DamageIncrease()
    {
        bulletDamage++;
    }

    public void SlowOn()
    {
        isSlow = true;
    }

    public void ShootBaseBullet(float delay, Vector3 direction, Vector3 spawnPosition, Quaternion spawnRotation, float weaponPower, Bullet prefab)
    {
        GameManagement.instance.DoubleShoot(delay, direction, spawnPosition, spawnRotation, weaponPower, prefab);
    }    

    public void Explode(float radius, GameObject vfx)
    {
        Instantiate(vfx, transform.position, vfx.transform.rotation);
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius); //depois tem que colocar pra targetar só enemy
        foreach (Collider e in enemies)
        {
            Enemy enemy = e.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                 enemy.TakeDamage(bulletDamage);
            }
        }
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] int damage = 1;
    [SerializeField] float initialForce = 10f;
    [SerializeField] int penetration;
    [SerializeField] bool isSlow;
    [SerializeField] float slowDuration;
    [SerializeField] float slowAmount;
    [SerializeField] float knockback;
    [SerializeField] float critChance;
    [SerializeField] bool isCrit;

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
                int damage = this.damage;

                if (isCrit)
                { damage *= 2; }

                enemy.TakeDamage(damage);
            }

            if (slowAmount > 0)
            { enemy.MultiplySpeed(1/slowAmount, slowDuration); }

            if (knockback > 0)
            { enemy.TakeKnockback(knockback); }

            if (penetration > 0)
            { penetration--; }
            else
            { Destroy(gameObject); }
        }
    }

    public void Shoot(Vector3 direction)
    {
        rigidbody.AddForce(direction.normalized * initialForce, ForceMode.VelocityChange);
        lifeSpan = lifeSpanLeft;
    }

    public void PenetrationOn()
    {
        penetration = 1;
    }

    public void DamageIncrease()
    {
        damage++;
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
                enemy.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

    private void RollCrit()
    { isCrit = Random.value < critChance; }

    public int Damage { get => damage; set => damage = value; }
    public float InitialForce { get => initialForce; set => initialForce = value; }
    public (float Amount, float Duration) Slow { get => (slowAmount, slowDuration); set => (slowAmount, slowDuration) = value; }
    public float Knockback { get => knockback; set => knockback = value; }
    public float CritChance { get => critChance; set => critChance = value; }
    public int Penetration { get => penetration; set => penetration = value; }

    static public Bullet Shoot(Bullet prefab, Vector3 position, Vector3 direction, HashSet<ITreeBulletUpgrade> upgrades)
    {
        Bullet bullet = Instantiate(prefab, position, Quaternion.LookRotation(direction));

        foreach (ITreeBulletUpgrade upgrade in upgrades)
        { upgrade.UpgradeBullet(bullet); }
        bullet.RollCrit();

        Debug.Log(direction);

        bullet.Shoot(direction);

        return bullet;
    }
}

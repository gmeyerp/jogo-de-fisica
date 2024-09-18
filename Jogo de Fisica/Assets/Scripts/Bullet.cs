using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] int bulletDamage = 1;
    [SerializeField] bool penetration;

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
}

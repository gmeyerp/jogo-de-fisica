using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;

    [SerializeField] private float lifeSpan = 5;
    private float lifeSpanLeft;
    private void Awake()
    {
        lifeSpanLeft = lifeSpan;
    }

    private void Update()
    {
        if (lifeSpanLeft > 0)
        {
            if (lifeSpanLeft > Time.deltaTime)
            { lifeSpanLeft -= Time.deltaTime; }
            else
            {
                lifeSpanLeft = 0;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.VelocityChange);
        lifeSpan = lifeSpanLeft;
    }
}

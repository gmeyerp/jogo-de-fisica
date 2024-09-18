using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [Header("General")]
    [SerializeField] int maxHealth = 5;
    [SerializeField] int health = 5;
    [SerializeField] int coinValue = 1;
    [SerializeField] float speed;

    [Header("Track")]
    [SerializeField] Track track;
    int currentWaypoint = 1;
    Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        direction = track.Waypoints[currentWaypoint].position - transform.position;
        if (direction.magnitude < 0.1f)
        {
            NextWaypoint();
        }
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        
        rb.MovePosition(transform.position + direction.normalized * speed * Time.fixedDeltaTime);
    }

    void NextWaypoint()
    {
        if (currentWaypoint < track.Waypoints.Length - 1)
            currentWaypoint++;
        else
        {
            GameManagement.instance.ReduceHealth();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        PlayerMovement.instance.TakeDamage();
    }

    public void Die()
    {
        GameManagement.instance.ChangeMoney(coinValue);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health-= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public Enemy Instantiate(Track track)
    {
        Enemy instance = Instantiate(this, track.StartWaypoint.position, track.StartWaypoint.rotation);
        instance.track = track;
        return instance;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    [SerializeField] int health = 5;
    [SerializeField] float speed;
    List<Transform> waypoints;
    [SerializeField] Rigidbody rb;
    int currentWaypoint = 1;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        waypoints = EnemySpawner.instance.GetWaypoints();
    }

    private void Update()
    {
        direction = waypoints[currentWaypoint].position - transform.position;
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
        if (currentWaypoint < waypoints.Count - 1)
            currentWaypoint++;
        else
        {
            GameManagement.instance.ReduceHealth();
            EnemySpawner.instance.RemoveEnemyFromMap();
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
        EnemySpawner.instance.RemoveEnemyFromMap();
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {

    }
}

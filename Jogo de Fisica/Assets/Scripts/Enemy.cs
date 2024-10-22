    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [Header("General")]
    [SerializeField] int maxHealth = 5;
    [SerializeField] int health = 5;
    [SerializeField] float speed;
    bool isSlowed;
    [SerializeField] Drop[] drops;

    [Header("Track")]
    [SerializeField] Track track;
    int nextWaypointIndex = 1;
    float distanceTravelled = 0;

    [Header("FX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject turretDamageVFX;

    Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        transform.LookAt(track.Waypoints[nextWaypointIndex].transform);
    }

    private void Update()
    {
        direction = track.Waypoints[nextWaypointIndex].position - transform.position;
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
        
        Transform nextWaypoint = track.Waypoints[nextWaypointIndex];
        float distanceToNextWaypoint = Vector3.Distance(transform.position, nextWaypoint.position);
        distanceTravelled = track.DistanceOf(nextWaypoint) - distanceToNextWaypoint;
    }

    void NextWaypoint()
    {
        if (nextWaypointIndex < track.Waypoints.Length - 1)
        {
            nextWaypointIndex++;
            transform.LookAt(track.Waypoints[nextWaypointIndex].transform);
        }
        else
        {
            GameManagement.instance.ReduceHealth();
            Instantiate(turretDamageVFX, transform.position, turretDamageVFX.transform.rotation);
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

    private void OnDestroy()
    {
        track.Remove(enemy: this);
    }

    void DealDamage()
    {
        PlayerMovement.instance.TakeDamage();
    }

    public void Die()
    {
        Instantiate(deathVFX, transform.position, deathVFX.transform.rotation);
        Destroy(gameObject);

        for (int i = 0; i < drops.Length; i++)
        {
            Instantiate(drops[i], transform.position, Quaternion.identity);
        }
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

    public float TrackPercentageCovered => distanceTravelled / track.TotalDistance;

    public void SlowDown(float amount, float duration)
    {
        isSlowed = true;
        speed *= amount;
    }

    IEnumerator ReturnSpeed(float amount, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (isSlowed)
        {
            speed /= amount;
            isSlowed = false;
        }
    }
}

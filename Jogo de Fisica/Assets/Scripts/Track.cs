using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private Dictionary<Transform, float> distances;

    [SerializeField] private List<Enemy> enemies;
    private Enemy first;

    private void Awake()
    {
        distances = new Dictionary<Transform, float>();
    }

    private void Start()
    {
        CalculateDistances();
    }

    private void CalculateDistances()
    {
        float totalDistance = 0;
        foreach (Transform waypoint in waypoints)
        {
            distances[waypoint] = totalDistance;
            totalDistance += waypoint.position.magnitude;
        }
    }

    public void Send(Enemy enemyPrefab)
    {
        Enemy enemyInstance = enemyPrefab.Instantiate(track: this);
        enemies.Add(enemyInstance);
    }

    public void Remove(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public float DistanceOf(Transform waypoint) => distances[waypoint];

    public Transform[] Waypoints => waypoints;

    public Transform StartWaypoint => Waypoints[0];
    public Transform EndWaypoint => Waypoints[waypoints.Length - 1];

    public float TotalDistance => distances[EndWaypoint];

    public int EnemiesLeft => enemies.Count;
}

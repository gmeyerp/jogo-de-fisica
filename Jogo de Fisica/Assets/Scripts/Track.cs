using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private List<Enemy> enemies;
    private float totalDistance;

    private void Start()
    {
        totalDistance = CalculateTotalDistance();
    }

    private float CalculateTotalDistance()
    {
        float totalDistance = 0;
        
        foreach (Transform waypoint in waypoints)
        { totalDistance += waypoint.position.magnitude; }

        return totalDistance;
    }

    public void Send(Enemy enemy)
    {
        Enemy instance = enemy.Instantiate(track: this);
        enemies.Add(instance);
    }

    public void Remove(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public Transform[] Waypoints => waypoints;

    public Transform StartWaypoint => Waypoints[0];
    public Transform EndWaypoint => Waypoints[waypoints.Length - 1];
}

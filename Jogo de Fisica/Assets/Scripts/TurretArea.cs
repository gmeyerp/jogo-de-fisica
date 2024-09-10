using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretArea : MonoBehaviour
{

    private List<TurretTarget> targets;
    private void Awake()
    {
        targets = new List<TurretTarget>();
    }

    private void Update()
    {
        Debug.Log($"In range: {targets.Count}");
    }

    public void Enter(TurretTarget gameObject)
    {
        targets.Add(gameObject);
    }

    public void Leave(TurretTarget gameObject)
    {
        targets.Remove(gameObject);
    }

    public int TargetCount => targets.Count;
    public TurretTarget First => targets.Count > 0 ? targets[0] : null;
}

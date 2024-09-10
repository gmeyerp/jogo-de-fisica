using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTarget : MonoBehaviour
{
    private List<TurretArea> targetedBy;
    private void Awake()
    {
        targetedBy = new List<TurretArea>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out TurretArea area))
        {
            targetedBy.Add(area);
            area.Enter(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out TurretArea area))
        {
            targetedBy.Remove(area);
            area.Leave(this);
        }
    }

    private void OnDestroy()
    {
        foreach (var area in targetedBy)
        {
            area.Leave(this);
        }
    }
}

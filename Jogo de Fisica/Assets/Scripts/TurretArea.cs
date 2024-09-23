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

    public void Enter(TurretTarget gameObject)
    {
        targets.Add(gameObject);
    }

    public void Leave(TurretTarget gameObject)
    {
        targets.Remove(gameObject);
    }

    public int TargetCount => targets.Count;
    public TurretTarget First
    {
        get
        {
            if (targets.Count == 0) return null;

            TurretTarget first = targets[0];
            Enemy firstIfEnemy = null;
            foreach (TurretTarget target in targets)
            {
                if (target.gameObject.TryGetComponent(out Enemy targetAsEnemy))
                {
                    if (false
                        // Priorizando inimigos
                        || (firstIfEnemy == null)
                        // Priorizando os inimigos mais próximos do fim
                        || (firstIfEnemy.TrackPercentageCovered < targetAsEnemy.TrackPercentageCovered) 
                    )
                    {
                        first = target;
                        firstIfEnemy = targetAsEnemy;
                    }
                }
            }

            return first;
        }
    }
}

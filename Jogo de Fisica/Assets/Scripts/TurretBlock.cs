using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretBlock : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;

    [SerializeField] private Transform mountPivot;
    private Transform riderOriginalParent;
    private Transform rider;

    public void Move(Vector3 force)
    {
        rigidbody.AddForce(force, ForceMode.Force);
    }

    public void Mount(Transform rider)
    {
        riderOriginalParent = rider.transform.parent;
        this.rider = rider;

        rider.transform.SetParent(mountPivot);
        rider.position = mountPivot.position;
        //rider.OnMount();
    }

    public void Dismount()
    {
        //rider.OnDismount(rigidbody.velocity);
        rider.transform.SetParent(riderOriginalParent);

        riderOriginalParent = null;
        rider = null;
    }
}

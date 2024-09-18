using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretBlock : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;

    [SerializeField] private Transform mountPivot;
    public Turret turret;
    //private Transform riderOriginalParent;
    private Transform rider;

    private void Update()
    {
        if (rider != null)
        {
            rider.SetPositionAndRotation(
                mountPivot.position,
                mountPivot.rotation
            );
        }
    }

    public void Move(Vector3 force, float maxSpeed)
    {
        if (rigidbody.velocity.magnitude < maxSpeed)
        {
            rigidbody.AddForce(force, ForceMode.Force);
        }
    }

    public void Mount(Transform rider)
    {
        this.rider = rider;

        //rider.transform.SetParent(mountPivot);
        //rider.position = mountPivot.position;
        //rider.OnMount();
    }

    public void Dismount()
    {
        //rider.OnDismount(rigidbody.velocity);
        //rider.transform.SetParent(riderOriginalParent);

        //riderOriginalParent = null;
        rider = null;
    }

    public Vector3 Velocity => rigidbody.velocity;
}

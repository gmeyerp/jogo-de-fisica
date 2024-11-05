using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCannon : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private TurretArea area;

    [Header("Weapon")]
    [SerializeField] private Rigidbody glove;
    [SerializeField] private float weaponPower = 10f;
    [SerializeField] private float weaponCooldown = 2f;
    private float weaponCooldownLeft;

    private void Update()
    {
        if (weaponCooldownLeft > Time.deltaTime)
        { weaponCooldownLeft -= Time.deltaTime; }
        else
        { weaponCooldownLeft = 0; }

        if (area.TargetCount > 0)
        {
            Vector3 target = area.First.transform.position;
            Vector3 direction = Vector3.Normalize(target - transform.position);

            Quaternion rotation = Quaternion.FromToRotation(transform.forward, direction);

            SpringJoint springJoint = glove.GetComponent<SpringJoint>();
            springJoint.connectedAnchor = rotation * springJoint.connectedAnchor;

            Quaternion newRotation = Quaternion.LookRotation(direction);
            transform.rotation = newRotation;
            glove.transform.rotation = newRotation;

            if (weaponCooldownLeft == 0)
            {
                Shoot(direction);
                weaponCooldownLeft = weaponCooldown;
            }
        }
    }

    private void Shoot(Vector3 direction)
    {
        Vector3 force = direction.normalized * weaponPower;
        glove.AddForce(force, ForceMode.Impulse);

        Debug.Log("shot");
    }
}

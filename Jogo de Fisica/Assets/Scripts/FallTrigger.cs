using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            PlayerMovement.instance.ApplyDamage();
            PlayerMovement.instance.transform.position = GameManagement.instance.GetSafePosition();
        }
    }
}

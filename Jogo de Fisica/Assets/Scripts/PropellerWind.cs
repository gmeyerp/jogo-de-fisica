using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerWind : MonoBehaviour
{
    [SerializeField] float force = 3f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement.instance.UpWind(force);
            Debug.Log("wind");
        }
    }
}

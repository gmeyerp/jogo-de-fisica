using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionIndicator : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float maxDistance = 20f;
    [SerializeField] LayerMask isGround;
    [SerializeField] GameObject positionPointer;
    [SerializeField] float pointerMinDistance = 0.6f;
    [SerializeField] float lerpSpeed = 0.1f;
    RaycastHit hit;
    float lineSize;

    void FixedUpdate()
    {
        //Remover quando corrigir bug de pulo duplo
        //if (PlayerMovement.instance.isGrounded)
        //{
        //    lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        //}
        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxDistance, isGround, QueryTriggerInteraction.Ignore))
        {
            lineSize = hit.distance;
            if (lineSize > pointerMinDistance)
            {
                positionPointer.SetActive(true);
                positionPointer.transform.position = hit.point;
                positionPointer.transform.rotation = Quaternion.LookRotation(-hit.normal);
            }
            lineRenderer.SetPosition(1, new Vector3(0,0, lineSize));
        }
        else
        {
            positionPointer.SetActive(false);
            lineSize = Mathf.Lerp(lineSize, maxDistance, lerpSpeed);
            lineRenderer.SetPosition(1, new Vector3(0, 0, lineSize));
        }
    }
}

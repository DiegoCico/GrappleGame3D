using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationGun : MonoBehaviour
{
    //calling in the script
    public GrappleGun grappling;

    private Quaternion desiredRotation;
    private float rotationSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        if (!grappling.IsGrappling())
        {
            desiredRotation = transform.parent.rotation;
        }
        else {
            desiredRotation = Quaternion.LookRotation(grappling.GetGrapplePoint() - transform.position);
        }

        //depending on the angle you will speed up faster or not 
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}

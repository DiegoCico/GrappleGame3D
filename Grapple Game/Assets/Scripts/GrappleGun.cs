using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{

    public LineRenderer lr;

    public Vector3 grapplePoint;

    public LayerMask whatisGrappleable;

    public Transform gunTip;
    public Transform cam;
    public Transform playerT;

    public float maxDistance = 100f;

    public SpringJoint joint;

    // awake runs before the game starts
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // button down 0 is the left button on mouse
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
           
        }
        //checks if we let go of the left mosue
        else if (Input.GetMouseButtonUp(0)) { 
            StopGrapple();
         
        }
    }

    // if you dont make a lateUpdate, the rope will have a delay
    // and it wont be attached to the gun
    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance)) {
            grapplePoint = hit.point;

            //adding joints to connect connect the grapple with the object
            joint = playerT.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            // finding the distance of the grapple point
            float distanceFrompoint = Vector3.Distance(playerT.position, grapplePoint);

            //distance grapple will try to keep from grapple poit
            joint.maxDistance = distanceFrompoint * 0.8f;
            joint.minDistance = distanceFrompoint * 0.25f;

            //play around with these
            //they change the pull, push
            //higher spring there is more pull and push
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            //how many position there are there
            lr.positionCount = 2;
        }
    }

    void DrawRope()
    {
        // if not grapple dont draw rope
        if (!joint) return;

        //finding the possition of the tip of the gun
        lr.SetPosition(0, gunTip.position);

        //finding the position of the object grappled
        lr.SetPosition(1, grapplePoint);
    }

    void StopGrapple()
    {
        //if not grapple there shouldnt be any
        lr.positionCount = 0;
        Destroy(joint);
    }

    //check if we are grappling or not
    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}

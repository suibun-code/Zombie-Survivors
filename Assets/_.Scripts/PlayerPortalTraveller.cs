using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortalTraveller : PortalTraveller
{
    public GameObject cameraFollowTarget;

    public override void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        //Calculate angle difference between portals to rotate camera
        var forwardA = transform.rotation * Vector3.forward;
        var forwardB = rot * Vector3.forward;
        var angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
        var angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;
        var angleDiff = Mathf.DeltaAngle(angleA, angleB);

        base.Teleport(fromPortal, toPortal, pos, rot);

        GetComponent<ThirdPersonController>().CameraYawOverride = angleDiff;
    }
}

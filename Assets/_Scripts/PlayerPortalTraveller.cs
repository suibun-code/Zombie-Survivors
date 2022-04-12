using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPortalTraveller : PortalTraveller
{
    public GameObject cameraFollowTarget;

    public override void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        base.Teleport(fromPortal, toPortal, pos, rot);

        cameraFollowTarget.transform.rotation = rot;
    }
}

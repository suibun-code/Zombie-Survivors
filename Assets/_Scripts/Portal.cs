using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public List<PortalTraveller> trackedTravellers = new List<PortalTraveller>();

    [SerializeField] Camera playerCam;

    public Portal linkedPortal;

    MeshRenderer portalDoor;

    Camera portalCam;

    RenderTexture viewTexture;

    [SerializeField] static bool playerTravelled = false;

    static bool VisibleFromCamera(Renderer renderer, Camera camera)
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
    }

    private void Awake()
    {
        portalDoor = GetComponent<MeshRenderer>();
        portalCam = GetComponentInChildren<Camera>();
        portalCam.enabled = false;
    }

    void Start()
    {
        CreateViewTexture();
    }

    private void Update()
    {
        if (PortalReset.leftTriggerZone)
        {
            playerTravelled = false;
            portalDoor.enabled = true;
            linkedPortal.portalDoor.enabled = true;
            PortalReset.leftTriggerZone = false;
        }

        Render();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < trackedTravellers.Count; i++)
        {
            PortalTraveller traveller = trackedTravellers[i];
            Transform travellerT = traveller.transform;

            Vector3 offsetFromPortal = travellerT.position - transform.position;
            int portalSide = System.Math.Sign(Vector3.Dot(offsetFromPortal, transform.forward));
            int portalSideOld = System.Math.Sign(Vector3.Dot(traveller.previousOffsetFromPortal, transform.forward));

            if(portalSide != portalSideOld)
            {
                portalDoor.enabled = false;
                linkedPortal.portalDoor.enabled = false;

                Matrix4x4 m = linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * travellerT.localToWorldMatrix;
                traveller.Teleport(transform, linkedPortal.transform, m.GetColumn(3), m.rotation);

                linkedPortal.OnTravellerEnterPortal(traveller);
                trackedTravellers.RemoveAt(i);
                i--;
            }
            else
            {
                traveller.previousOffsetFromPortal = offsetFromPortal;
            }
            
        }
    }

    void CreateViewTexture()
    {
        //portalCam = GetComponentInChildren<Camera>();
        //if (portalCam == null) return;
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
                viewTexture.Release();

            viewTexture = new RenderTexture(Screen.width, Screen.height, 24);
            portalCam.targetTexture = viewTexture;
            linkedPortal.portalDoor.material.mainTexture = viewTexture;
        }
    }

    public void Render()
    {
        if (playerTravelled)
            return;

        if (!VisibleFromCamera(linkedPortal.portalDoor, playerCam))
        {
            var testTexture = new Texture2D(1, 1);
            testTexture.SetPixel(0, 0, Color.red);
            testTexture.Apply();
            linkedPortal.portalDoor.material.mainTexture = testTexture;
            return;
        }
        linkedPortal.portalDoor.material.mainTexture = viewTexture;

        portalDoor.enabled = false;
        CreateViewTexture();

        Matrix4x4 m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        portalCam.Render();
        portalDoor.enabled = true;
    }

    void OnTravellerEnterPortal(PortalTraveller traveller)
    {
        if (traveller.tag == "Player" && playerTravelled == false)
        {
            playerTravelled = true;
        }

        if (!trackedTravellers.Contains(traveller))
        {
            traveller.EnterPortalThreshold();
            traveller.previousOffsetFromPortal = traveller.transform.position - transform.position;
            trackedTravellers.Add(traveller);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PortalTraveller traveller = other.GetComponent<PortalTraveller>();

        if (traveller)
        {
            OnTravellerEnterPortal(traveller);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PortalTraveller traveller = other.GetComponent<PortalTraveller>();

        if (traveller && trackedTravellers.Contains(traveller))
        {
            traveller.ExitPortalThreshold();
            trackedTravellers.Remove(traveller);
        }
    }
}

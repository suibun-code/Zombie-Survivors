using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalReset : MonoBehaviour
{
    public static bool leftTriggerZone = false;

    private List<Collider> otherCols;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("leftTriggerZone TRUE");
            leftTriggerZone = true;
        }
    }
}

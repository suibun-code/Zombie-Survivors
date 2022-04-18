using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCanvas : MonoBehaviour
{
    public GameObject switchTo;
    public GameObject switchFrom;

    public void Switch()
    {
        switchFrom.SetActive(false);
        switchTo.SetActive(true);
    }
}

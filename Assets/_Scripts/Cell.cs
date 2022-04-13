using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private bool tested = false;
    [SerializeField] private bool walkable = true;

    private MeshRenderer mRenderer;

    private void Awake()
    {
        mRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void SetCellColor(Color color)
    {
        mRenderer.material.color = color;
    }
}

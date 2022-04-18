using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Color defaultColor;
    public Color nodeHighlightedColor;
    public Color nodeObstacleColor;

    private MeshRenderer mRenderer;

    public PathNode node;

    private void Awake()
    {
        mRenderer = GetComponentInChildren<MeshRenderer>();
        ResetCellColor();
    }

    public void SetCellColor(Color color)
    {
        mRenderer.material.color = color;
    }

    public void SetCellHighlighted()
    {
        if (node == null)
        {
            Debug.Log("NODE NULL");
            return;
        }

        if (node.HasObstacle())
            mRenderer.material.color = nodeObstacleColor;
        else
            mRenderer.material.color = nodeHighlightedColor;
    }

    public void ResetCellColor()
    {
        if (node == null)
        {
            return;
        }

        if (node.HasObstacle())
            mRenderer.material.color = nodeObstacleColor;
        else
            mRenderer.material.color = defaultColor;
    }
}

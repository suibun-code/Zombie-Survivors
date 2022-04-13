using System;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public GridDisplay gridDisplay;

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;

    private float cellSize;

    private Vector3 originPosition;

    public PathNode[,] nodeArray;
    public List<PathNode> walkableNodes;

    public Board(Vector3 originPosition, int width, int height, float cellSize, GridDisplay gridDisplay)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        this.gridDisplay = gridDisplay;

        this.gridDisplay.board = this;

        nodeArray = new PathNode[width, height];
        walkableNodes = new List<PathNode>();

        for (int x = 0; x < nodeArray.GetLength(0); x++)
            for (int z = 0; z < nodeArray.GetLength(1); z++)
            {
                nodeArray[x, z] = new PathNode(this, x, z);
                walkableNodes.Add(nodeArray[x, z]);

                if (this.gridDisplay != null)
                    this.gridDisplay.CreateCell(x, z, width, height, cellSize, nodeArray[x, z].GetWalkable());
            }

        OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) =>
        {
            Debug.Log(eventArgs.x + eventArgs.y);
        };
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, z) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition + originPosition).x / cellSize);
        z = Mathf.FloorToInt((worldPosition + originPosition).y / cellSize);
    }

    public void SetGridObject(int x, int z, PathNode value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            nodeArray[x, z] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = z });
        }
    }

    public void TriggerGridObjectChanged(int x, int z)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = z });
    }

    public void SetGridObject(Vector3 worldPosition, PathNode value)
    {
        int x, z;
        GetXY(worldPosition, out x, out z);
        SetGridObject(x, z, value);
    }

    public PathNode GetGridObject(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            return nodeArray[x, z];
        }
        else
        {
            Debug.Log("X: " + x + " Y: " + z);
            return default(PathNode);
        }
    }

    public PathNode GetGridObject(Vector3 worldPosition)
    {
        int x, z;
        GetXY(worldPosition, out x, out z);
        return GetGridObject(x, z);
    }
}

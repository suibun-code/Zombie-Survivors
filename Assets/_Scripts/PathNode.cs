using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Board grid;
    public int x;
    public int z;

    public int xWithParentOffset;
    public int zWithParentOffset;

    public int gCost;
    public int hCost;
    public int fCost;

    private bool walkable;
    private bool inPlayerRange;
    private bool hasObstacle;

    public PathNode previousNode;
    public Cell cell;

    public PathNode(Board grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.z = y;

        walkable = true;
    }

    public void SetWalkable(bool isWalkable)
    {
        this.walkable = isWalkable;
    }

    public bool GetWalkable()
    {
        return walkable;
    }

    public void SetInPlayerRange(bool inPlayerRange)
    {
        this.inPlayerRange = inPlayerRange;
    }

    public bool InPlayerRange()
    {
        return inPlayerRange;
    }

    public void SetHasObstacle(bool hasTrap)
    {
        this.hasObstacle = hasTrap;
    }

    public bool HasObstacle()
    {
        return hasObstacle;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(x, z, 0);
    }

    public override string ToString()
    {
        return x + "," + z;
    }


}

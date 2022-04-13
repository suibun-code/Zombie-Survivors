using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridHolder : Singleton<GridHolder>
{
    private int highestX;
    private int highestZ;

    public List<BoardManager> boardManagers;
    public static int managerCount = 0;

    //pathnode with its parent position offset, so it is in worldspace (with 0.5 offset for mesh to be in the center)
    public Dictionary<PathNode, Vector3Int> tempNodes;
    public PathNode[,] allNodes;

    void Awake()
    {
        MakeAllNodesArray();
    }

    private void MakeAllNodesArray()
    {
        Dictionary<PathNode, Vector3Int> tempNodes = new Dictionary<PathNode, Vector3Int>();
        boardManagers = new List<BoardManager>(managerCount);

        for (int i = 0; i < managerCount; i++)
            boardManagers.Add(transform.GetChild(i).GetComponent<BoardManager>());

        foreach (BoardManager manager in boardManagers)
        {
            if (manager.board == null)
                continue;

            foreach (PathNode node in manager.board.nodeArray)
            {
                if (node != null)
                {
                    int xWithOffset = node.x + (int)manager.transform.position.x;
                    int zWithOffset = node.z + (int)manager.transform.position.z;

                    if (highestX < xWithOffset)
                        highestX = xWithOffset;

                    if (highestZ < zWithOffset)
                        highestZ = zWithOffset;

                    //Debug.Log((node.x + manager.transform.position.x) + " | " + (node.z + manager.transform.position.z));

                    tempNodes.Add(node, new Vector3Int((int)manager.transform.position.x, 0, (int)manager.transform.position.z));
                }
            }
        }

        allNodes = new PathNode[highestX + 1, highestZ + 1];

        foreach (KeyValuePair<PathNode, Vector3Int> pair in tempNodes)
        {
            int xWithOffset = pair.Key.x + pair.Value.x;
            int zWithOffset = pair.Key.z + pair.Value.z;

            //Debug.Log(xWithOffset + " | " + zWithOffset);

            allNodes[xWithOffset, zWithOffset] = pair.Key;
            allNodes[xWithOffset, zWithOffset].xWithParentOffset = xWithOffset;
            allNodes[xWithOffset, zWithOffset].zWithParentOffset = zWithOffset;
        }

        foreach (PathNode node in allNodes)
        {
            if (node == null)
                continue;

            TextMeshPro textMesh = node.cell.GetComponentInChildren<TextMeshPro>();
            textMesh.text = "[" + node.xWithParentOffset + ", " + node.zWithParentOffset + "]";
        }
    }

    public PathNode GetNode(int x, int z)
    {
        //if the location is out of range of the grid array then return
        if (allNodes.GetLength(0) < x || allNodes.GetLength(1) < z
            || 0 > allNodes.GetLength(0) || 0 > allNodes.GetLength(1))
        {
            Debug.Log("Node out of range");
        }

        return allNodes[x, z];
    }
}

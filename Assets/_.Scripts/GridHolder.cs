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
    public List<PathNode> highlightedNodes;

    protected override void Awake()
    {
        base.Awake();

        MakeAllNodesArray();
    }

    private void Start()
    {
        boardManagers = new List<BoardManager>(managerCount);
        highlightedNodes = new List<PathNode>();
    }

    private void Update()
    {
        HighlightNode();
    }

    private void LateUpdate()
    {
        CheckHighlightNodes();
    }

    private void HighlightNode()
    {
        //highlight node being looked at
        PathNode nodeToHighlight = GetNode(ThirdPersonController.currGridLookAt);
        //Debug.Log("Node to highlight: " + ThirdPersonController.currGridLookAt);
        if (nodeToHighlight != null && !highlightedNodes.Contains(nodeToHighlight))
        {
            nodeToHighlight.cell.SetCellHighlighted();

            highlightedNodes.Add(nodeToHighlight);
            //Debug.Log("ADDED NODE TO HIGHLIGHT LIST X: " + nodeToHighlight.xWithParentOffset + " Z: " + nodeToHighlight.zWithParentOffset);
        }
    }

    private void CheckHighlightNodes()
    {
        for (int i = 0; i < highlightedNodes.Count; i++)
        {
            if (ThirdPersonController.currGridLookAt.x != highlightedNodes[i].xWithParentOffset
                || ThirdPersonController.currGridLookAt.z != highlightedNodes[i].zWithParentOffset)
            {
                highlightedNodes[i].cell.ResetCellColor();

                highlightedNodes.Remove(highlightedNodes[i]);
            }
        }
    }

    private void MakeAllNodesArray()
    {
        Dictionary<PathNode, Vector3Int> tempNodes = new Dictionary<PathNode, Vector3Int>();

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

                    tempNodes.Add(node, new Vector3Int((int)manager.transform.position.x, 0, (int)manager.transform.position.z));
                }
            }
        }

        allNodes = new PathNode[highestX + 1, highestZ + 1];

        foreach (KeyValuePair<PathNode, Vector3Int> pair in tempNodes)
        {
            int xWithOffset = pair.Key.x + pair.Value.x;
            int zWithOffset = pair.Key.z + pair.Value.z;

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

            node.SetCellNode();
        }
    }

    public PathNode GetNode(int x, int z)
    {
        //if the location is out of range of the grid array then return
        if (allNodes.GetLength(0) <= x ||
            allNodes.GetLength(1) <= z ||
            x < 0 ||
            z < 0)
        {
            return null;
        }

        //Debug.Log("RETURNED NODE: " + allNodes[x, z]);
        return allNodes[x, z];
    }

    public PathNode GetNode(Vector2Int pos)
    {
        return GetNode(pos.x, pos.y);
    }

    public PathNode GetNode(Vector3Int pos)
    {
        return GetNode(pos.x, pos.z);
    }
}

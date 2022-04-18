using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingManager : MonoBehaviour
{
    public List<Tilemap> unWalkableTilemaps;

    private Pathfinding pathfinding;

    public int gridWidth;
    public int gridHeight;

    private void Awake()
    {
        pathfinding = new Pathfinding(transform.position, gridWidth, gridHeight, 1);
    }

    private void Start()
    {
        //SetUnWalkableTiles();
    }

    //private void SetUnWalkableTiles()
    //{
    //    foreach (var tilemap in unWalkableTilemaps)
    //    {
    //        for (int i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++)
    //            for (int j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++)
    //                MakeTileUnWalkable(tilemap, i, j);
    //    }
    //}

    //private void MakeTileUnWalkable(Tilemap tilemap, int i, int j)
    //{
    //    if (i > pathfinding.GetGrid().nodeArray.GetLength(0) || j > pathfinding.GetGrid().nodeArray.GetLength(1))
    //        return;

    //    Vector3Int localPos = new Vector3Int(i, j, (int)tilemap.transform.position.z);
    //    Vector3 worldPos = tilemap.CellToWorld(localPos);

    //    if (tilemap.GetSprite(localPos) != null)
    //    {
    //        GridDisplay.instance.SetUnWalkableColor((int)worldPos.x, (int)worldPos.y);
    //        pathfinding.GetGrid().nodeArray[(int)worldPos.x, (int)worldPos.y].SetWalkable(false);
    //        pathfinding.GetGrid().walkableNodes.Remove(pathfinding.GetGrid().nodeArray[(int)worldPos.x, (int)worldPos.y]);
    //    }
    //}
}

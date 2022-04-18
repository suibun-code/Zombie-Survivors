using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : Singleton<ObstacleManager>
{
    int obstacleCost;
    public GameObject obstaclePrefab;

    public void SpawnObstacle(int posX, int posZ)
    {
        //if the grid display is off you cant spawn traps
        if (GridDisplay.display == false)
            return;

        //if the trap location is out of range of the grid array then return
        if (GridHolder.instance.allNodes.GetLength(0) <= posX ||
            GridHolder.instance.allNodes.GetLength(1) <= posZ ||
            posX < 0 ||
            posZ < 0)
        {
            return;
        }

        //if node is null, return
        if (GridHolder.instance.allNodes[posX, posZ] == null)
            return;

        //if the grid node already has an obstacle on it, return
        if (GridHolder.instance.allNodes[posX, posZ].HasObstacle())
            return;

        //if the grid node isn't in range of the player, return
        if (!GridHolder.instance.allNodes[posX, posZ].InPlayerRange())
            return;

        //spawn obstacle
        if (!PlayerStats.SubtractMoney(25))
            return;

        GameObject obstacle = Instantiate(obstaclePrefab, transform);
        obstacle.transform.position = new Vector3(posX + 0.5f, obstacle.transform.position.y + 0.25f, posZ + 0.5f);
        GridHolder.instance.allNodes[posX, posZ].SetHasObstacle(true);
        GridHolder.instance.allNodes[posX, posZ].cell.ResetCellColor();

        Debug.Log("SPAWNED OBSTACLES");
    }
}

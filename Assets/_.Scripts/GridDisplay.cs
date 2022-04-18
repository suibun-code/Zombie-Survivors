using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GridDisplay : MonoBehaviour
{
    public GameObject player;

    public Board board;

    [SerializeField] public static bool display = true;
    [SerializeField] public bool limitToPlayerReach = true;

    public int gridDepth;

    public GameObject[,] allCells;

    private void Start()
    {
        SetDisplayGrid(display);
    }

    private void Update()
    {
        if (allCells == null || display == false)
            return;

        if (limitToPlayerReach)
        {
            //do once every 60 frames
            if (Utility.RateLimiter(60))
            {
                for (int i = 0; i < allCells.GetLength(0); i++)
                    for (int j = 0; j < allCells.GetLength(1); j++)
                    {
                        if (Vector3.Distance(player.transform.position, allCells[i, j].transform.position) < gridDepth)
                        {
                            board.nodeArray[i, j].SetInPlayerRange(true);
                            allCells[i, j].SetActive(true);
                        }
                        else
                        {
                            board.nodeArray[i, j].SetInPlayerRange(false);
                            allCells[i, j].SetActive(false);
                        }
                    }
            }
        }
    }

    public void ToggleGridDisplay()
    {
        if (display == true)
        {
            for (int i = 0; i < allCells.GetLength(0); i++)
                for (int j = 0; j < allCells.GetLength(1); j++)
                {
                    allCells[i, j].SetActive(false);
                    display = false;
                }
        }
        else
        {
            for (int i = 0; i < allCells.GetLength(0); i++)
                for (int j = 0; j < allCells.GetLength(1); j++)
                {
                    allCells[i, j].SetActive(true);
                    display = true;
                }
        }
    }

    public void SetDisplayGrid(bool display)
    {
        for (int i = 0; i < allCells.GetLength(0); i++)
            for (int j = 0; j < allCells.GetLength(1); j++)
            {
                if (display)
                {
                    allCells[i, j].SetActive(true);
                }
                else if (!display)
                    allCells[i, j].SetActive(false);
            }
    }

    public void ToggleDisplay()
    {
        if (!display)
            display = true;
        else
            display = false;

        SetDisplayGrid(display);
    }

    public void CreateCell(int x, int z, int width, int height, float cellSize, bool walkable)
    {
        if (allCells == null)
            allCells = new GameObject[width, height];

        allCells[x, z] = Instantiate(Resources.Load("Prefabs/Cell") as GameObject, transform);

        Transform cellT = allCells[x, z].GetComponent<Transform>();

        //Spawn depending on parent position, place in array, and with half width and height offset because the pivot is in the center instead of bottom left, spawning them incorrectly
        cellT.position = new Vector3(transform.position.x + (x * cellSize) + cellSize / 2, transform.position.y, transform.position.z + (z * cellSize) + cellSize / 2);

        //TextMeshPro textMesh = allCells[x, z].GetComponentInChildren<TextMeshPro>();
        //textMesh.text = "[" + x + ", " + z + "]";

        allCells[x, z].SetActive(false);

        board.GetGridObject(x, z).cell = allCells[x, z].GetComponent<Cell>();
        //board.GetGridObject(x, z).cell.node = board.GetGridObject(x, z);
    }

    public void SetUnWalkableColor(int x, int z)
    {
        SpriteRenderer spriteRenderer = allCells[x, z].GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = new Color(0.9607843f, 0.2313726f, 0.3411765f, 0.3490196f);
    }
}
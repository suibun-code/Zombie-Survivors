using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private GridDisplay gridDisplay;

    public Board board;

    public int gridWidth;
    public int gridHeight;

    private void Awake()
    {
        GridHolder.managerCount++;
        gridDisplay = GetComponentInChildren<GridDisplay>();
        board = new Board(transform.position, gridWidth, gridHeight, 1, gridDisplay);
    }
}

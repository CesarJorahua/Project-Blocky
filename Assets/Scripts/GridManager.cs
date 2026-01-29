using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 6;
    public int cols = 5;

    public List<Block> blockPrefabs;
    public Transform gridRoot;

    private Block[,] grid;

    private const float CELL_WIDTH = 1;
    private const float CELL_HEIGHT = 1;



}

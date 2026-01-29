using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{
    public int rows = 6;
    public int cols = 5;

    public List<GameObject> blockPrefabs;
    public Transform gridRoot;

    private Block[,] grid;

    //Block width 128px/100 units per pixel
    private const float CELL_WIDTH = 1.28f;

    //Block height 112px/100 units per pixel
    private const float CELL_HEIGHT = 1.12f;

    private bool inputLocked = false;
    private void Start()
    {
        InitializeGrid();
        RepositionGrid();
    }

    private void RepositionGrid()
    {
        float width = (cols - 1) * CELL_WIDTH;
        float height = (rows - 1) * CELL_HEIGHT;

        gridRoot.position = new Vector3(-width / 2f, height / 2f, 0f);
    }

    private void InitializeGrid()
    {
        grid = new Block[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                CreateBlock(r, c);
            }
        }
    }

    private void CreateBlock(int row, int col)
    {
        var block = Instantiate(blockPrefabs[Random.Range(0,blockPrefabs.Count)], gridRoot);

        block.GetComponent<Block>().Init(row, col);
        block.transform.position = GetWorldPosition(row, col);
        block.GetComponent<SpriteRenderer>().sortingOrder = rows - row;

        grid[row, col] = block.GetComponent<Block>();
    }

    private Vector3 GetWorldPosition(int row, int col)
    {
        float x = col * CELL_WIDTH;
        float y = -row * CELL_HEIGHT;

        return gridRoot.transform.position + new Vector3(x, y, 0f);
    }

    public void OnClickBlock(Block start)
    {
        if (inputLocked)
            return;
        HashSet<Block> collected = new HashSet<Block>();
        BlockLookup(start.Row, start.Col, start.Color, collected);
        if(collected.Count == 0)
            return;
        StartCoroutine(ResolveTurn(collected));
    }

    private void BlockLookup(int row, int col, BlockColor color, HashSet<Block> collected)
    {
        if (row < 0 || row >= rows || col < 0 || col >= cols)
            return;
        Block block = grid[row, col];
        if (block == null || block.Color != color || !collected.Add(block))
            return;
        BlockLookup(row + 1, col, color, collected);
        BlockLookup(row - 1, col, color, collected);
        BlockLookup(row, col + 1, color, collected);
        BlockLookup(row, col - 1, color, collected);
    }

    private IEnumerator ResolveTurn(HashSet<Block> collected)
    {
        inputLocked = true;

        // Remove blocks
        foreach (var block in collected)
        {
            grid[block.Row, block.Col] = null;
            Destroy(block.gameObject);
        }

        ScoreAndMoveManager.Instance.AddScore(collected.Count);
        ScoreAndMoveManager.Instance.UseMove();

        yield return new WaitForSeconds(1f);
        MoveBlocksGravity();
        RefillGrid();
        inputLocked = false;
    }

    private void MoveBlocksGravity()
    {
        for (int col = 0; col < cols; col++)
        {
            for (int row = rows - 1; row >= 0; row--)
            {
                if (grid[row, col] == null)
                {
                    for (int above = row - 1; above >= 0; above--)
                    {
                        if (grid[above, col] != null)
                        {
                            MoveBlock(above, row, col);
                            break;
                        }
                    }
                }
            }
        }
    }
    private void MoveBlock(int fromRow, int toRow, int col)
    {
        var block = grid[fromRow, col];

        grid[fromRow, col] = null;
        grid[toRow, col] = block;

        block.Row = toRow;
        block.transform.position = GetWorldPosition(toRow, col);
    }

    private void RefillGrid()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (grid[r, c] == null)
                    CreateBlock(r, c);
            }
        }
    }
}

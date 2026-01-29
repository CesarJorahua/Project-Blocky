using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Main class managing the game grid.
/// Also do block creation, positioning, removal, and pseudo gravity mechanics.
/// Handles player interactions through block clicks and coordinates the turn resolution process.
/// </summary>
public class GridManager : MonoBehaviour
{
    public List<GameObject> blockPrefabs;
    public Transform gridRoot;

    private Block[,] grid;

    private bool _inputLocked = false;

    /// <summary>
    /// Starting point of the gameplay initialization.
    /// </summary>
    private void Start()
    {
        InitializeGrid();
        RepositionGrid();
    }

    /// <summary>
    /// Creates all blocks for the game grid with random colors and initializes their positions.
    /// </summary>
    private void InitializeGrid()
    {
        grid = new Block[Constants.ROWS, Constants.COLUMNS];

        for (int r = 0; r < Constants.ROWS; r++)
        {
            for (int c = 0; c < Constants.COLUMNS; c++)
            {
                CreateBlock(r, c);
            }
        }
    }

    /// <summary>
    /// Repositions the grid root to center it on the screen based on grid dimensions.
    /// </summary>
    private void RepositionGrid()
    {
        float width = (Constants.COLUMNS - 1) * Constants.CELL_WIDTH;
        float height = (Constants.ROWS - 1) * Constants.CELL_HEIGHT;

        gridRoot.position = new Vector3(-width / 2f, height / 2f, 0f);
    }

    /// <summary>
    /// Instantiates a block at the specified grid coordinates with a random color prefab.
    /// </summary>
    /// <param name="row">The row index for block creation.</param>
    /// <param name="col">The column index for block creation.</param>
    private void CreateBlock(int row, int col)
    {
        var block = Instantiate(blockPrefabs[Random.Range(0,blockPrefabs.Count)], gridRoot);

        block.GetComponent<Block>().Init(row, col);
        block.transform.position = GetWorldPosition(row, col);
        block.GetComponent<SpriteRenderer>().sortingOrder = Constants.ROWS - row;

        grid[row, col] = block.GetComponent<Block>();
    }

    /// <summary>
    /// Converts grid coordinates (row, column) to world space position based on cell dimensions and grid root position.
    /// </summary>
    /// <param name="row">The row index.</param>
    /// <param name="col">The column index.</param>
    /// <returns>The world position as a Vector3.</returns>
    private Vector3 GetWorldPosition(int row, int col)
    {
        float x = col * Constants.CELL_WIDTH;
        float y = -row * Constants.CELL_WIDTH;

        return gridRoot.transform.position + new Vector3(x, y, 0f);
    }

    /// <summary>
    /// Handles block click events by finding all connected blocks of the same color and initiating turn resolution.
    /// Ignores input if currently locked during animation or processing.
    /// </summary>
    /// <param name="start">The block that was clicked.</param>
    public void OnClickBlock(Block start)
    {
        if (_inputLocked)
            return;
        HashSet<Block> collected = new HashSet<Block>();
        BlockLookup(start.Row, start.Col, start.Color, collected);
        if(collected.Count == 0)
            return;
        StartCoroutine(ResolveTurn(collected));
    }

    /// <summary>
    /// Main recursion to find all connected blocks of the same color starting from the given position.
    /// Uses flood fill algorithm to identify contiguous blocks.
    /// </summary>
    /// <param name="row">The current row index to check.</param>
    /// <param name="col">The current column index to check.</param>
    /// <param name="color">The color to match for connected blocks.</param>
    /// <param name="collected">HashSet to store all found connected blocks.</param>
    private void BlockLookup(int row, int col, BlockColor color, HashSet<Block> collected)
    {
        if (row < 0 || row >= Constants.ROWS || col < 0 || col >= Constants.COLUMNS)
            return;
        Block block = grid[row, col];
        if (block == null || block.Color != color || !collected.Add(block))
            return;
        BlockLookup(row + 1, col, color, collected);
        BlockLookup(row - 1, col, color, collected);
        BlockLookup(row, col + 1, color, collected);
        BlockLookup(row, col - 1, color, collected);
    }

    /// <summary>
    /// Turn resolution that implies collection of block, update visuals(texts), and apply gravity/refill mechanics.
    /// Also locks user interaction while resolve the turn.
    /// 
    /// </summary>
    /// <param name="collected">The set of blocks to remove.</param>
    private IEnumerator ResolveTurn(HashSet<Block> collected)
    {
        _inputLocked = true;

        // Remove blocks
        foreach (var block in collected)
        {
            grid[block.Row, block.Col] = null;
            Destroy(block.gameObject);
        }

        ScoreAndMoveManager.Instance.AddScore(collected.Count);
        ScoreAndMoveManager.Instance.UseMove();

        yield return new WaitForSeconds(Constants.AWAIT_RESOLUTION);
        MoveBlocksGravity();
        RefillGrid();
        _inputLocked = false;
    }

    /// <summary>
    /// Applies pseudo gravity to blocks by moving them down to fill empty spaces within each column.
    /// </summary>
    private void MoveBlocksGravity()
    {
        for (int col = 0; col < Constants.COLUMNS; col++)
        {
            for (int row = Constants.ROWS - 1; row >= 0; row--)
            {
                if (grid[row, col] != null) continue;
                for (int above = row - 1; above >= 0; above--)
                {
                    if (grid[above, col] == null) continue;
                    MoveBlock(above, row, col);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Moves a block from one row to another within the same column.
    /// Updates the block's position and grid references.
    /// </summary>
    /// <param name="fromRow">The source row index.</param>
    /// <param name="toRow">The destination row index.</param>
    /// <param name="col">The column index (remains constant).</param>
    private void MoveBlock(int fromRow, int toRow, int col)
    {
        var block = grid[fromRow, col];

        grid[fromRow, col] = null;
        grid[toRow, col] = block;

        block.Row = toRow;
        block.transform.position = GetWorldPosition(toRow, col);
    }

    /// <summary>
    /// Refills empty grid positions with new blocks of random colors.
    /// </summary>
    private void RefillGrid()
    {
        for (int r = 0; r < Constants.ROWS; r++)
        {
            for (int c = 0; c < Constants.COLUMNS; c++)
            {
                if (grid[r, c] == null)
                    CreateBlock(r, c);
            }
        }
    }
}

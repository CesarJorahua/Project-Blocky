using UnityEngine;
using UnityEngine.InputSystem;

///TODO: Object pooling implementation for block creation 
/// <summary>
/// Class representing a single block in the grid.
/// </summary>
public class Block : MonoBehaviour
{
    public int Row { get; set; }
    public int Col { get; set; }

    public BlockColor Color;

    private GridManager _grid;
    public void Init(int row, int col)
    {
        Row = row;
        Col = col;
        _grid = FindFirstObjectByType<GridManager>();
    }

    public void OnClickedBlock()
    {
        _grid.OnClickBlock(this);
    }
}

[SerializeField]
public enum BlockColor
{
    Green,
    Purple,
    Yellow,
    Brown,
    Pink
}

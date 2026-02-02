using UnityEngine;
using ProjectBlocky.Utils;

/// <summary>
/// Class representing a single block in the grid.
/// </summary>
public class Block : MonoBehaviour
{
    public int Row { get; set; }
    public int Col { get; set; }

    public BlockColor color;
    public void Init(int row, int col, BlockColor blockColor)
    {
        Row = row;
        Col = col;
        this.color = blockColor;
    }
}

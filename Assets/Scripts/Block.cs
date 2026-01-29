using UnityEngine;
using UnityEngine.InputSystem;

public class Block : MonoBehaviour
{
    public int Row { get; set; }
    public int Col { get; set; }

    [SerializeField] public BlockColor Color;

    private GridManager _grid;
    public void Init(int row, int col)
    {
        Row = row;
        Col = col;
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

using UnityEngine;

public class Block : MonoBehaviour
{
    public int Row { get; set; }
    public int Col { get; set; }

    [SerializeField] private BlockColor color;

    public BlockColor Color
    {
        get => color;
        set => color = value;
    }

    private GridManager grid;

    public void Init(int row, int col, BlockColor color)
    {
        Row = row;
        Col = col;
        Color = color;
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

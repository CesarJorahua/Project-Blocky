using UnityEngine;

public class Block : MonoBehaviour
{
    public int Row { get; set; }
    public int Col { get; set; }

    [SerializeField] private BlockColor color;

    private GridManager grid;

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

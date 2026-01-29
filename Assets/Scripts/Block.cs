using UnityEngine;

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

    public void OnMouseDown()
    {
        Debug.Log($"[{GetType()}] Clicked block : " + gameObject.name, this);
        Debug.Log($"[{GetType()}] Block Row : " + Row);
        Debug.Log($"[{GetType()}] Block Column : " + Col);
        //_grid.OnClickBlock(this);
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

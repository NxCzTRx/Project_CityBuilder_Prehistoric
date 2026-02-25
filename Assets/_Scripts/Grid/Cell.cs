using UnityEngine;

public class Cell
{
    public Vector2Int Position { get; private set; }
    public bool IsOccupied { get; private set; }
    public bool IsWalkable { get; set; }

    public Cell(Vector2Int position)
    {
        Position = position;
        IsOccupied = false;
        IsWalkable = true;
    }
}

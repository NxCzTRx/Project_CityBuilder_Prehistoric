using UnityEngine;

public static class GridUtils
{
    public static Vector2Int WorldToGridPosition(Vector3 worldPosition, float cellSize)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);
        return new Vector2Int(x, y);
    }

    public static Vector3 GridToWorldPosition(Vector3 gridPosition, float cellSize)
    {
        float x = gridPosition.x * cellSize + cellSize / 2f;
        float y = gridPosition.y * cellSize + cellSize / 2f;
        return new Vector3(x, y, 0);
    }
}

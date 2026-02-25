using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridWidth = 20;
    [SerializeField] private int gridHeight = 20;
    [SerializeField] private float cellSize = 1f;
    
    private Cell[,] _grid;

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        _grid = new Cell[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                _grid[x, y] = new Cell(new Vector2Int(x, y));
            }
        }
    }

    public bool IsValidPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < gridWidth && 
               position.y >= 0 && position.y < gridHeight;
    }

    public Cell GetCell(Vector2Int position) =>
        !IsValidPosition(position) ? null : _grid[position.x, position.y];
    
    #region Debug

    private void OnDrawGizmos()
    {
        if (_grid is null) return;
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, y * cellSize, 0);
                Gizmos.color = _grid[x, y].IsWalkable ? Color.green : Color.red;
                Gizmos.DrawWireCube(cellPosition, Vector3.one * cellSize);
            }
        }
    }

    #endregion
}

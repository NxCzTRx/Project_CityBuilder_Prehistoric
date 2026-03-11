using UnityEngine;

namespace _Scripts.Grid
{
    public class GridManager
    {
        private readonly int _gridWidth = 20;
        private readonly int _gridHeight = 20;
        private readonly float _cellSize = 1f;
        public float CellSize => _cellSize;
    
        public GridManager(int width, int height, float cellSize)
        {
            _gridWidth = width;
            _gridHeight = height;
            _cellSize = cellSize;

            CreateGrid();
        }
        
        private Cell[,] _grid;

        private void CreateGrid()
        {
            _grid = new Cell[_gridWidth, _gridHeight];

            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    _grid[x, y] = new Cell(new Vector2Int(x, y));
                }
            }
        }

        private bool IsValidPosition(Vector2Int position)
        {
            return position.x >= 0 && position.x < _gridWidth && 
                   position.y >= 0 && position.y < _gridHeight;
        }

        public Cell GetCell(Vector2Int position) =>
            !IsValidPosition(position) ? null : _grid[position.x, position.y];

        
        #region Debug

        private void OnDrawGizmos()
        {
            if (_grid is null) return;
        
            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    Vector3 cellPosition = new Vector3(
                        _grid[x, y].Position.x * _cellSize + _cellSize / 2f, 
                        _grid[x, y].Position.y * _cellSize + _cellSize / 2f, 
                        0f);
                    Gizmos.color = _grid[x, y].IsWalkable ? Color.green : Color.red;
                    Gizmos.DrawWireCube(cellPosition, Vector3.one * _cellSize);
                }
            }
        }

        #endregion
    }
}

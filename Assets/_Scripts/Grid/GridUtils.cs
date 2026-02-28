using UnityEngine;

namespace _Scripts.Grid
{
    public static class GridUtils
    {
        /// <summary>
        /// Returns the grid position corresponding to the given world position, based on the cell size.
        /// </summary>
        /// <param name="worldPosition"></param>
        /// <param name="cellSize"></param>
        /// <returns></returns>
        public static Vector2Int WorldToGridPosition(Vector3 worldPosition, float cellSize)
        {
            int x = Mathf.FloorToInt(worldPosition.x / cellSize);
            int y = Mathf.FloorToInt(worldPosition.y / cellSize);
            return new Vector2Int(x, y);
        }

        /// <summary>
        /// Returns a position in world space corresponding to the center of the cell at the given grid position.
        /// </summary>
        /// <param name="gridPosition"></param>
        /// <param name="cellSize"></param>
        /// <returns></returns>
        public static Vector3 CellToWorldPosition(Vector2Int gridPosition, float cellSize)
        {
            float x = gridPosition.x * cellSize + cellSize / 2f;
            float y = gridPosition.y * cellSize + cellSize / 2f;
            return new Vector3(x, y, 0);
        }

        /// <summary>
        /// Returns a position in world space corresponding to the center of the area
        /// defined by the given grid origin (bottom-left cell) and size.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="cellSize"></param>
        /// <returns></returns>
        public static Vector3 GridToWorldPosition(Vector2Int origin, Vector2Int size, float cellSize)
        {
            float width = size.x * cellSize;
            float height = size.y * cellSize;
            
            return new Vector3(
                origin.x * cellSize + width / 2f, 
                origin.y * cellSize + height / 2f, 
                0);
        }
        
        /// <summary>
        /// Returns the grid position corresponding to the given mouse position in screen space,
        /// based on the camera and cell size.
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <param name="camera"></param>
        /// <param name="cellSize"></param>
        /// <returns></returns>
        public static Vector2Int GetCellFromMousePosition(
            Vector2 mousePosition,
            UnityEngine.Camera camera,
            float cellSize)
        { 
            return WorldToGridPosition(camera.ScreenToWorldPoint(mousePosition), cellSize);
        } 
    }
}

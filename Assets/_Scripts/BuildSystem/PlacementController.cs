using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.BuildSystem
{
    public class PlacementController : MonoBehaviour
    {
        private GridManager _gridManager;
        
        [SerializeField] private BuildingSO _buildingSO; //Quitar serialize

        public void Init(GridManager gridManager)
        {
            _gridManager =  gridManager;
        }

        /// <summary>
        /// Returns the grid origin and world center of a building placement based on the center cell.
        /// </summary>
        /// <param name="centerCell"></param>
        /// <returns></returns>
        public (Vector2Int gridOrigin, Vector2 worldCenter) GetBuildingPlacement(Cell centerCell)
        {
            var gridOrigin = new Vector2Int(
                centerCell.Position.x - _buildingSO.BuildingWidth / 2,
                centerCell.Position.y - _buildingSO.BuildingHeight / 2); ;

            var worldCenter = ((Vector2)GridUtils.GridToWorldPosition(
                gridOrigin,
                new Vector2Int(_buildingSO.BuildingWidth, _buildingSO.BuildingHeight)
                , _gridManager.CellSize));

            return (gridOrigin, worldCenter);
        }
        
        public bool IsValidPlacement(Vector2Int origin)
        {
            for (int x = 0; x < _buildingSO.BuildingWidth; x++)
            {
                for (int y = 0; y < _buildingSO.BuildingHeight; y++)
                {
                    var cell = _gridManager.GetCell(new Vector2Int(origin.x + x, origin.y + y));
                    if (cell == null || cell.IsOccupied)
                        return false;
                }
            }

            return true;
        }
    }
}

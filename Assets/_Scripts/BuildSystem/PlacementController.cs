using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.BuildSystem
{
    public class PlacementController
    {
        private GridManager _gridManager;
        
        public PlacementController(GridManager gridManager)
        {
            _gridManager =  gridManager;
        }

        /// <summary>
        /// Returns the grid origin and world center of a building placement based on the center cell.
        /// </summary>
        /// <param name="centerCell"></param>
        /// <returns></returns>
        public (Vector2Int gridOrigin, Vector2 worldCenter) GetBuildingPlacement(Cell centerCell, BuildingSO buildingSO)
        {
            var gridOrigin = new Vector2Int(
                centerCell.Position.x - buildingSO.BuildingWidth / 2,
                centerCell.Position.y - buildingSO.BuildingHeight / 2); ;

            var worldCenter = ((Vector2)GridUtils.GridToWorldPosition(
                gridOrigin,
                new Vector2Int(buildingSO.BuildingWidth, buildingSO.BuildingHeight)
                , _gridManager.CellSize));

            return (gridOrigin, worldCenter);
        }
        
        public bool IsValidPlacement(Vector2Int origin, BuildingSO buildingSO)
        {
            for (int x = 0; x < buildingSO.BuildingWidth; x++)
            {
                for (int y = 0; y < buildingSO.BuildingHeight; y++)
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

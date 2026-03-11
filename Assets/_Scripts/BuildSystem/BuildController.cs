
using _Scripts.BuildSystem.Building;
using _Scripts.Core;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.BuildSystem
{
    public class BuildController : MonoBehaviour
    {
        private GridManager _gridManager;
        private ObjectResolver _objectResolver;
    
        public void Init(GridManager gridManager, ObjectResolver objectResolver)
        {
            _gridManager =  gridManager;
            _objectResolver = objectResolver;
        }
    
        public void Build(Vector2Int gridOrigin, Vector2 worldCenter, BuildingSO buildingSO)
        {
            Cell workCell = null;
            
            int midX = buildingSO.BuildingWidth / 2;

            for (int x = 0; x < buildingSO.BuildingWidth; x++)
            {
                for (int y = 0; y < buildingSO.BuildingHeight; y++)
                {
                    var cell = _gridManager.GetCell(gridOrigin + new Vector2Int(x, y));
                    cell.IsOccupied = true;
                    cell.IsWalkable = false;

                    if (x == midX && y == 0) 
                    {
                        cell.IsWalkable = true;
                        workCell = cell;
                    }
                }
            }
        
            var building = Instantiate(buildingSO.BuildingPrefab, worldCenter, Quaternion.identity);
            building.GetComponent<BuildingEntity>().Init(buildingSO, workCell, _objectResolver);
        }
    }
}

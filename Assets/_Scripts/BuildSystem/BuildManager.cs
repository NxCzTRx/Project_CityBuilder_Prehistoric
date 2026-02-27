using _Scripts.Grid;
using _Scripts.Input;
using UnityEngine;

namespace _Scripts.BuildSystem
{
    public class BuildManager : MonoBehaviour
    {
        [SerializeField] private PlacementController placementController;
        [SerializeField] private GhostBuilding ghostBuilding;
        [SerializeField] private BuildController buildController;
        
        [SerializeField] private BuildingSO selectedBuildingData; //Take off serialize, test purpose only
        
        private (Vector2Int gridOrigin, Vector2 worldCenter) _currentPlacement = new(Vector2Int.zero, Vector2.zero);
    
        //TEMP
        [SerializeField] private GridManager gridManager;
        [SerializeField] private InputManager inputManager;
        //
        private void OnEnable()
        {
            inputManager.OnMouseMove += HandleBuildingPlacement;
            inputManager.OnBuild += HandleBuildRequest;
        }

        private void Start()
        {
            placementController.Init(gridManager);
            buildController.Init(gridManager);
        }
        
        private void HandleBuildRequest()
        {
            if (placementController.IsValidPlacement(_currentPlacement.gridOrigin))
            {
                buildController.Build(
                    _currentPlacement.gridOrigin, _currentPlacement.worldCenter, selectedBuildingData);
            }
        }

        private void HandleBuildingPlacement(Vector2 mousePosition)
        {
            var cellPos = GetCellFromMousePosition(mousePosition);
            var centerCell = gridManager.GetCell(cellPos);

            if (centerCell == null) return;
        
            var placement = placementController.GetBuildingPlacement(centerCell);
            _currentPlacement = placement;
            
            ghostBuilding.MoveGhost(_currentPlacement.worldCenter);
            ghostBuilding.SetValidPlacementColor(placementController.IsValidPlacement(_currentPlacement.gridOrigin));
        }
    
        private Vector2Int GetCellFromMousePosition(Vector2 mousePosition)
        {
            return GridUtils.WorldToGridPosition(
                UnityEngine.Camera.main.ScreenToWorldPoint(mousePosition), gridManager.CellSize);
        }
    
        private void OnDisable()
        {
            inputManager.OnMouseMove -= HandleBuildingPlacement;
            inputManager.OnBuild -= HandleBuildRequest;
        }
    }
}

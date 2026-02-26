using _Scripts.Grid;
using _Scripts.Input;
using UnityEngine;

namespace _Scripts.BuildSystem
{
    public class BuildManager : MonoBehaviour
    {
        [SerializeField] private PlacementController placementController;
        [SerializeField] private GhostBuilding ghostBuilding;
    
        //TEMP
        [SerializeField] private GridManager gridManager;
        [SerializeField] private InputManager inputManager;
        //
        private void OnEnable()
        {
            inputManager.OnMouseMove += HandleBuildingPlacement;
        }

        private void Start()
        {
            placementController.Init(gridManager);
        }

        private void HandleBuildingPlacement(Vector2 mousePosition)
        {
            var cellPos = GetCellFromMousePosition(mousePosition);
            var centerCell = gridManager.GetCell(cellPos);

            if (centerCell == null) return;
        
            var placement = placementController.GetBuildingPlacement(centerCell);
            ghostBuilding.MoveGhost(placement.worldCenter);
            ghostBuilding.SetValidPlacementColor(placementController.IsValidPlacement(placement.gridOrigin));
        }
    
        private Vector2Int GetCellFromMousePosition(Vector2 mousePosition)
        {
            return GridUtils.WorldToGridPosition(
                UnityEngine.Camera.main.ScreenToWorldPoint(mousePosition), gridManager.CellSize);
        }
    
        private void OnDisable()
        {
            inputManager.OnMouseMove -= HandleBuildingPlacement;
        }
    }
}

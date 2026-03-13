using _Scripts.Core;
using _Scripts.Grid;
using _Scripts.Input;
using _Scripts.ResourcesSystem;
using UnityEngine;

namespace _Scripts.BuildSystem
{
    public class BuildManager : MonoBehaviour
    {
        private PlacementController _placementController;
        [SerializeField] private GhostBuilding ghostBuilding;
        [SerializeField] private BuildController buildController;
        
        private BuildingSO _selectedBuildingData;
        
        private (Vector2Int gridOrigin, Vector2 worldCenter) _currentPlacement = new(Vector2Int.zero, Vector2.zero);
        
        private GridManager _gridManager;
        private InputManager _inputManager;
        private GameResourcesManager _gameResourcesManager;

        private UnityEngine.Camera _mainCamera;

        public void Init(ObjectResolver objectResolver)
        {
            _gridManager = objectResolver.Resolve<GridManager>();
            _inputManager = objectResolver.Resolve<InputManager>();
            _gameResourcesManager = objectResolver.Resolve<GameResourcesManager>();

            _placementController = new PlacementController(_gridManager);
            buildController.Init(_gridManager, objectResolver);

            _mainCamera = UnityEngine.Camera.main;
        }
        
        private void HandleBuildRequest()
        {
            if (!_placementController.IsValidPlacement(_currentPlacement.gridOrigin, _selectedBuildingData) ||
                !_gameResourcesManager.CanAfford(_selectedBuildingData.BuildingCost))
                return;
            
            _gameResourcesManager.RemoveResources(_selectedBuildingData.BuildingCost);
            buildController.Build(
                _currentPlacement.gridOrigin, _currentPlacement.worldCenter, _selectedBuildingData);
        }

        private void HandleBuildingPlacement(Vector2 mousePosition)
        {
            var cellPos = GridUtils.GetCellFromMousePosition(
                mousePosition, _mainCamera, _gridManager.CellSize);
            
            var centerCell = _gridManager.GetCell(cellPos);

            if (centerCell == null) return;
        
            _currentPlacement = _placementController.GetBuildingPlacement(
                centerCell, _selectedBuildingData);

            ghostBuilding.MoveGhost(_currentPlacement.worldCenter);
            ghostBuilding.SetValidPlacementColor(
                _placementController.IsValidPlacement(_currentPlacement.gridOrigin, _selectedBuildingData));
        }

        public void EnableBuildManager(BuildingSO buildingSo)
        {
            Cursor.visible = false;
            ghostBuilding.gameObject.SetActive(true);
            _selectedBuildingData = buildingSo;
            _inputManager.OnMouseMove += HandleBuildingPlacement;
            _inputManager.OnBuild += HandleBuildRequest;
        }

        public void DisableBuildManager()
        {
            Cursor.visible = true;
            ghostBuilding.gameObject.SetActive(false);
            _inputManager.OnMouseMove -= HandleBuildingPlacement;
            _inputManager.OnBuild -= HandleBuildRequest;
        }
    
        private void OnDisable()
        {
            _inputManager.OnMouseMove -= HandleBuildingPlacement;
            _inputManager.OnBuild -= HandleBuildRequest;
        }
    }
}

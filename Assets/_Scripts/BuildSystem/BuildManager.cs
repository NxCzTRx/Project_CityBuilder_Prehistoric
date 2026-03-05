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
        
        [SerializeField] private BuildingSO selectedBuildingData; //Take off serialize, test purpose only
        
        private (Vector2Int gridOrigin, Vector2 worldCenter) _currentPlacement = new(Vector2Int.zero, Vector2.zero);
        
        private GridManager _gridManager;
        private InputManager _inputManager;
        private GameResourcesManager _gameResourcesManager;

        private bool _isPreviouslyInitialized = false;
        private UnityEngine.Camera _mainCamera;
        
        public void Init(ObjectResolver objectResolver)
        {
            _gridManager = objectResolver.Resolve<GridManager>();
            _inputManager = objectResolver.Resolve<InputManager>();
            _gameResourcesManager = objectResolver.Resolve<GameResourcesManager>();

            _placementController = new PlacementController(_gridManager);
            buildController.Init(_gridManager);

            _mainCamera = UnityEngine.Camera.main;

            InitializeInput();
        }
        
        private void OnEnable()
        {
            if (!_isPreviouslyInitialized)
                return;
            
            InitializeInput();
        }
        
        private void InitializeInput()
        {
            _inputManager.OnMouseMove += HandleBuildingPlacement;
            _inputManager.OnBuild += HandleBuildRequest;
            _isPreviouslyInitialized = true;
        }
        
        private void HandleBuildRequest()
        {
            if (!_placementController.IsValidPlacement(_currentPlacement.gridOrigin, selectedBuildingData) ||
                !_gameResourcesManager.CanAfford(selectedBuildingData.BuildingCost))
                return;
            
            _gameResourcesManager.RemoveResources(selectedBuildingData.BuildingCost);
            buildController.Build(
                _currentPlacement.gridOrigin, _currentPlacement.worldCenter, selectedBuildingData);
        }

        private void HandleBuildingPlacement(Vector2 mousePosition)
        {
            var cellPos = GridUtils.GetCellFromMousePosition(
                mousePosition, _mainCamera, _gridManager.CellSize);
            
            var centerCell = _gridManager.GetCell(cellPos);

            if (centerCell == null) return;
        
            _currentPlacement = _placementController.GetBuildingPlacement(
                centerCell, selectedBuildingData);

            ghostBuilding.MoveGhost(_currentPlacement.worldCenter);
            ghostBuilding.SetValidPlacementColor(
                _placementController.IsValidPlacement(_currentPlacement.gridOrigin, selectedBuildingData));
        }
        
        public void SetBuildManagerActive(bool isActive)
        {
            if (isActive)
            {
                Cursor.visible = false;
                ghostBuilding.gameObject.SetActive(true);
            }
            else
            {
                Cursor.visible = true;
                ghostBuilding.gameObject.SetActive(false);
            }
        }
    
        private void OnDisable()
        {
            _inputManager.OnMouseMove -= HandleBuildingPlacement;
            _inputManager.OnBuild -= HandleBuildRequest;
        }
    }
}

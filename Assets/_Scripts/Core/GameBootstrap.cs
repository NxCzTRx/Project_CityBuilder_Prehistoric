using _Scripts.AI.Entities.Pawn;
using _Scripts.BuildSystem;
using _Scripts.Camera;
using _Scripts.Core.GameMode;
using _Scripts.Core.UpdateManagement;
using _Scripts.Core.GameMode.Modes;
using _Scripts.Grid;
using _Scripts.Input;
using _Scripts.ResourcesSystem;
using _Scripts.ResourcesSystem.Resources;
using _Scripts.ResourcesSystem.UI;
using UnityEngine;

namespace _Scripts.Core
{
    [DefaultExecutionOrder(-100)]
    public class GameBootstrap : MonoBehaviour
    {
        private readonly ObjectResolver _objectResolver = new();
        
        private GridManager _gridManager;
        private GameModeManager _gameModeManager;
        private GameResourcesManager _gameResourcesManager;
        
        [SerializeField] ResourceStock[] initialResources;

        private UpdateManager _updateManager;
        
        [SerializeField] private SelectableController selectableController;
        [SerializeField] private BuildManager buildManager;
        [SerializeField] private InputManager inputManager;
        
        [SerializeField] private CameraController cameraController;
        
        [SerializeField] private PawnEntity pawnEntityPrefab; //TEST
        
        [Header("UI")]
        [SerializeField] private PlayerResourcesUI playerResourcesUI;
        
        

        private void Awake()
        {
            var updateManagerGO = new GameObject("UpdateManager");
            _updateManager = updateManagerGO.AddComponent<UpdateManager>();
            
            _gridManager = new GridManager(20, 20, 1f);
            _gameModeManager = new GameModeManager(new DefaultGameMode(inputManager));
            _gameResourcesManager = new GameResourcesManager(initialResources);
            
            pawnEntityPrefab = Instantiate(pawnEntityPrefab, Vector3.zero, Quaternion.identity);
            
            _objectResolver.RegisterInstance(buildManager);
            _objectResolver.RegisterInstance(_gridManager);
            _objectResolver.RegisterInstance(inputManager);
            _objectResolver.RegisterInstance(_gameResourcesManager);
        
            selectableController.Init(_objectResolver);
            buildManager.Init(_objectResolver);
            cameraController.Init(_objectResolver);
            pawnEntityPrefab.Init(_objectResolver);
            
            playerResourcesUI.Init(_gameResourcesManager);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        //Test method, will be triggered by UI button for now
        public void ChangeGameMode()
        {
            _gameModeManager.ChangeGameMode(new BuildGameMode(inputManager, buildManager));
        }

        private void OnDestroy()
        {
            _objectResolver.Clear();
        }
    }
}

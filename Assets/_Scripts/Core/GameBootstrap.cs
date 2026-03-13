using System;
using System.Collections;
using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Scheduling;
using _Scripts.BuildSystem;
using _Scripts.BuildSystem.Building.Housing;
using _Scripts.Camera;
using _Scripts.Core.DayCycle;
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
        private GameCycleManager _gameCycleManager;
        private PawnScheduler _pawnScheduler;
        private GameModeManager _gameModeManager;
        private GameResourcesManager _gameResourcesManager;
        private PawnRegistry _pawnRegistry;
        private HousingRegistry _housingRegistry;
        
        private PawnSpawner _pawnSpawner; //TEST
        
        [SerializeField] ResourceStock[] initialResources;

        private UpdateManager _updateManager;
        
        [SerializeField] private SelectableController selectableController;
        [SerializeField] private BuildManager buildManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private PawnSpawner pawnSpawnerPrefab;
        
        [SerializeField] private CameraController cameraController;
        
        [Header("UI")]
        [SerializeField] private PlayerResourcesUI playerResourcesUI;
        
        private void Awake()
        {
            var updateManagerGO = new GameObject("UpdateManager");
            _updateManager = updateManagerGO.AddComponent<UpdateManager>();

            var pawnSpawner = Instantiate(pawnSpawnerPrefab);
            
            _gridManager = new GridManager(20, 20, 1f);
            _gameCycleManager = new GameCycleManager();
            _gameModeManager = new GameModeManager(new DefaultGameMode(), inputManager, buildManager);
            _gameResourcesManager = new GameResourcesManager(initialResources);
            _pawnRegistry = new PawnRegistry();
            _housingRegistry = new HousingRegistry();
            _pawnScheduler = new PawnScheduler(_gameCycleManager, _pawnRegistry);
            
            _objectResolver.RegisterInstance(buildManager);
            _objectResolver.RegisterInstance(_gridManager);
            _objectResolver.RegisterInstance(_gameCycleManager);
            _objectResolver.RegisterInstance(inputManager);
            _objectResolver.RegisterInstance(_gameResourcesManager);
            _objectResolver.RegisterInstance(_pawnRegistry);
            _objectResolver.RegisterInstance(_housingRegistry);
            _objectResolver.RegisterInstance(_pawnScheduler);
            _objectResolver.RegisterInstance(pawnSpawner);
        
            _gameCycleManager.Init();
            selectableController.Init(_objectResolver);
            buildManager.Init(_objectResolver);
            cameraController.Init(_objectResolver);
            pawnSpawner.Init(_objectResolver);

            _pawnSpawner = pawnSpawner; //TEST
            
            playerResourcesUI.Init(_gameResourcesManager);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        //Test methods, will be triggered by UI button for now
        
        public void ChangeGameModeToDefault()
        {
            _gameModeManager.ChangeGameMode(new DefaultGameMode());
        }
        
        public void SpawnPawn() //TEST
        {
            _pawnSpawner.Spawn(Vector2.zero);
        }
        //

        private void OnDestroy()
        {
            _objectResolver.Clear();
        }
    }
}

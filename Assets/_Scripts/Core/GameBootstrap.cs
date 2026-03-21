using System;
using System.Collections;
using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Roles;
using _Scripts.AI.Entities.Pawn.Scheduling;
using _Scripts.BuildSystem;
using _Scripts.BuildSystem.Building.Housing;
using _Scripts.BuildSystem.InitialBuildings;
using _Scripts.Camera;
using _Scripts.Core.DayCycle;
using _Scripts.Core.GameMode;
using _Scripts.Core.UpdateManagement;
using _Scripts.Core.GameMode.Modes;
using _Scripts.DisasterSystem;
using _Scripts.Grid;
using _Scripts.ImmigrationSystem;
using _Scripts.Input;
using _Scripts.NotificationSystem;
using _Scripts.ResourcesSystem;
using _Scripts.ResourcesSystem.Resources;
using _Scripts.TechTreeSystem;
using _Scripts.TechTreeSystem.TechEra;
using _Scripts.UI.Gameplay;
using UnityEngine;
using PlayerResourcesUI = _Scripts.UI.Gameplay.PlayerResourcesUI;

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
        private DisasterManager _disasterManager;
        private TechTreeManager _techTreeManager;
        private ImmigrationManager _immigrationManager;
        private NotificationManager _notificationManager;
        private PawnRegistry _pawnRegistry;
        private HousingRegistry _housingRegistry;
        private RoleProductionRegistry _roleProductionRegistry;
        private GameConditionManager _gameConditionManager;
        
        private PawnSpawner _pawnSpawner;

        [SerializeField] private TechEraSo[] techEras;
        [SerializeField] ResourceStock[] initialResources;
        [SerializeField] private InitialBuildingsSO initialBuildingsSO;
        [SerializeField] private int initialPawns;

        private UpdateManager _updateManager;
        
        [SerializeField] private SelectableController selectableController;
        [SerializeField] private BuildManager buildManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private PawnSpawner pawnSpawnerPrefab;
        
        [SerializeField] private CameraController cameraController;
        
        [Header("UI")]
        [SerializeField] private GameplayUI gameplayUI;
        
        private void Awake()
        {
            var updateManagerGo = new GameObject("UpdateManager");
            _updateManager = updateManagerGo.AddComponent<UpdateManager>();

            var pawnSpawner = Instantiate(pawnSpawnerPrefab);
            
            _gridManager = new GridManager(50, 50, 1f);
            _gameModeManager = new GameModeManager(new DefaultGameMode(), inputManager, buildManager);
            _gameResourcesManager = new GameResourcesManager(initialResources);
            _techTreeManager = new TechTreeManager(techEras);
            _pawnRegistry = new PawnRegistry();
            _housingRegistry = new HousingRegistry();
            _immigrationManager = new ImmigrationManager(new Vector2(0,0));
            _disasterManager = new DisasterManager();
            _gameCycleManager = new GameCycleManager(_disasterManager);
            _pawnScheduler = new PawnScheduler(_gameCycleManager, _pawnRegistry);
            _roleProductionRegistry = new RoleProductionRegistry();
            _notificationManager = new NotificationManager();
            
            _objectResolver.RegisterInstance(buildManager);
            _objectResolver.RegisterInstance(_gridManager);
            _objectResolver.RegisterInstance(_gameCycleManager);
            _objectResolver.RegisterInstance(inputManager);
            _objectResolver.RegisterInstance(_gameResourcesManager);
            _objectResolver.RegisterInstance(_techTreeManager);
            _objectResolver.RegisterInstance(_immigrationManager);
            _objectResolver.RegisterInstance(_pawnRegistry);
            _objectResolver.RegisterInstance(_housingRegistry);
            _objectResolver.RegisterInstance(_pawnScheduler);
            _objectResolver.RegisterInstance(pawnSpawner);
            _objectResolver.RegisterInstance(_roleProductionRegistry);
            _objectResolver.RegisterInstance(gameplayUI);
            _objectResolver.RegisterInstance(_notificationManager);
        
            _gameCycleManager.Init();
            selectableController.Init(_objectResolver);
            buildManager.Init(_objectResolver);
            cameraController.Init(_objectResolver, new Vector2(25, 25));
            pawnSpawner.Init(_objectResolver);
            _disasterManager.Init(_objectResolver);
            _techTreeManager.Init(_objectResolver);
            _immigrationManager.Init(_objectResolver);
            _gameConditionManager = new GameConditionManager(_objectResolver);

            _pawnSpawner = pawnSpawner; //TEST
            
            gameplayUI.Init(_objectResolver);
            
            foreach (var initial in initialBuildingsSO.Buildings)
                buildManager.ManualBuildRequest(initial.GridOrigin, initial.BuildingSo);

            StartCoroutine(SpawnWithDelay(initialPawns));
        }
        
        private IEnumerator SpawnWithDelay(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _pawnSpawner.Spawn(new Vector2(0, 0));
                yield return new WaitForSeconds(0.3f);
            }
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnDestroy()
        {
            _objectResolver.Clear();
        }
    }
}

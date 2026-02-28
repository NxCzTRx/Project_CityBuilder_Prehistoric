using System;
using _Scripts.BuildSystem;
using _Scripts.Camera;
using _Scripts.Core.GameMode;
using _Scripts.Core.GameMode.Modes;
using _Scripts.Grid;
using _Scripts.Input;
using UnityEngine;

namespace _Scripts.Core
{
    [DefaultExecutionOrder(-100)]
    public class GameBootstrap : MonoBehaviour
    {
        private readonly ObjectResolver _objectResolver = new();
        
        private GridManager _gridManager;
        private GameModeManager _gameModeManager;
        
        [SerializeField] private BuildManager buildManager;
        [SerializeField] private InputManager inputManager;
        
        [SerializeField] private CameraController cameraController;

        private void Awake()
        {
            _gridManager = new GridManager(20, 20, 1f);
            _gameModeManager = new GameModeManager(new DefaultGameMode(inputManager), inputManager);
            
            _objectResolver.RegisterInstance(buildManager);
            _objectResolver.RegisterInstance(_gridManager);
            _objectResolver.RegisterInstance(inputManager);
        
            buildManager.Init(_objectResolver);
            cameraController.Init(_objectResolver);
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
            _objectResolver.UnregisterInstance<BuildManager>();
            _objectResolver.UnregisterInstance<GridManager>();
            _objectResolver.UnregisterInstance<InputManager>();
        }
    }
}

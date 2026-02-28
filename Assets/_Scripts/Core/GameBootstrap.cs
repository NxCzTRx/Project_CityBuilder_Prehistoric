using System;
using _Scripts.BuildSystem;
using _Scripts.Camera;
using _Scripts.Grid;
using _Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Core
{
    [DefaultExecutionOrder(-100)]
    public class GameBootstrap : MonoBehaviour
    {
        private readonly ObjectResolver _objectResolver = new();
        
        private GridManager _gridManager;
        
        [SerializeField] private BuildManager buildManager;
        [SerializeField] private InputManager inputManager;
        
        [SerializeField] private CameraController cameraController;

        private void Awake()
        {
            _gridManager = new GridManager(20, 20, 1f);
            
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

        private void OnDestroy()
        {
            _objectResolver.UnregisterInstance<BuildManager>();
            _objectResolver.UnregisterInstance<GridManager>();
            _objectResolver.UnregisterInstance<InputManager>();
        }
    }
}

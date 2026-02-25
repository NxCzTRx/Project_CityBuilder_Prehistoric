using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace _Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
    
        private PlayerInput _playerInput;
    
        public event Action<Vector2> OnCameraMove;
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                _playerInput = GetComponent<PlayerInput>();
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnEnable()
        {
            _playerInput.actions["MoveCamera"].performed += MoveCameraPerformed;
            _playerInput.actions["MoveCamera"].canceled += OMoveCameraCanceled;
        }

        private void MoveCameraPerformed(InputAction.CallbackContext ctx)
        {
            var inputVector = ctx.ReadValue<Vector2>();
            OnCameraMove?.Invoke(inputVector);
        }
    
        private void OMoveCameraCanceled(InputAction.CallbackContext ctx)
        {
            OnCameraMove?.Invoke(Vector2.zero);
        }

        private void OnDisable()
        {
            _playerInput.actions["MoveCamera"].performed -= MoveCameraPerformed;
            _playerInput.actions["MoveCamera"].canceled -= OMoveCameraCanceled;
        }
    }
}

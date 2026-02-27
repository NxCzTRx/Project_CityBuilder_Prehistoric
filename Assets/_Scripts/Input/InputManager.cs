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
        public event Action<Vector2> OnMouseMove;
        public event Action OnBuild;
        
    
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
            _playerInput.actions["MoveCamera"].canceled += MoveCameraCanceled;
            
            _playerInput.actions["Build"].started += BuildPerformed;
            
            _playerInput.actions["MouseMovement"].performed += MoveMousePerformed; //MANY CALLS
        }

        private void BuildPerformed(InputAction.CallbackContext ctx)
        {
            OnBuild?.Invoke();
        }

        private void MoveMousePerformed(InputAction.CallbackContext ctx)
        {
            var mousePosition = ctx.ReadValue<Vector2>();
            OnMouseMove?.Invoke(mousePosition);
        }

        private void MoveCameraPerformed(InputAction.CallbackContext ctx)
        {
            var inputVector = ctx.ReadValue<Vector2>();
            OnCameraMove?.Invoke(inputVector);
        }
    
        private void MoveCameraCanceled(InputAction.CallbackContext ctx)
        {
            OnCameraMove?.Invoke(Vector2.zero);
        }

        private void OnDisable()
        {
            _playerInput.actions["MoveCamera"].performed -= MoveCameraPerformed;
            _playerInput.actions["MoveCamera"].canceled -= MoveCameraCanceled;
            
            _playerInput.actions["Build"].started -= BuildPerformed;
            
            _playerInput.actions["MouseMovement"].performed -= MoveMousePerformed;
        }
    }
}

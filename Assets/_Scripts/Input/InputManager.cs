using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace _Scripts.Input
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInput _playerInput;
    
        public event Action<Vector2> OnCameraMove;
        public event Action<Vector2> OnMouseMove;
        public event Action OnBuild;
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            _playerInput.actions["MoveCamera"].performed += MoveCameraPerformed;
            _playerInput.actions["MoveCamera"].canceled += MoveCameraCanceled;
            
            _playerInput.actions["Build"].started += BuildPerformed;
            
            _playerInput.actions["MouseMovement"].performed += MoveMousePerformed;
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

using System.Collections;
using _Scripts.Core;
using _Scripts.Input;
using UnityEngine;

namespace _Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float accelerationTime;
    
        private Vector3 currentDirection = Vector3.zero;
        private Vector3 inputDirection = Vector3.zero;
        private float t = 0f;
        
        private bool _isPreviouslyInitialized = false;
        
        private InputManager _inputManager;
    
        public void Init(ObjectResolver objectResolver)
        {
            _inputManager = objectResolver.Resolve<InputManager>();
            
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
            _inputManager.OnCameraMove += HandleCameraMove;
            _isPreviouslyInitialized = true;
        }

        void Update()
        {
            if (t < 1f)
            {
                float easedT = t * t * (3f - 2f * t);
                currentDirection = currentDirection + (inputDirection - currentDirection) * easedT;
                t += Time.deltaTime / accelerationTime;

                if (t >= 1f)
                {
                    t = 1f;
                    currentDirection = inputDirection;
                }
            }
        
            transform.position += currentDirection * (speed * Time.deltaTime);
        }

        private void HandleCameraMove(Vector2 inputVector)
        {
            inputDirection = inputVector;
            t = 0f;
        }
        
        private void OnDisable()
        {
            _inputManager.OnCameraMove -= HandleCameraMove;
        }
    }
}

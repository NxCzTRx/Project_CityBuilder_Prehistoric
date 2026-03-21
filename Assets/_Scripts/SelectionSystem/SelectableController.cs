using _Scripts.Core;
using _Scripts.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SelectableController : MonoBehaviour
{
    private InputManager _inputManager;
    private ISelectable _currentSelectable;
    
    private UnityEngine.Camera _camera;

    private bool _isInitialized = false;
    
    public void Init(ObjectResolver objectResolver)
    {
        _inputManager = objectResolver.Resolve<InputManager>();
        _camera = Camera.main;
        _isInitialized = true;
        
        _inputManager.OnSelect -= Select;
        _inputManager.OnSelect += Select;
    }

    private void OnEnable()
    {
        if (!_isInitialized) return;

        _inputManager.OnSelect += Select;
    }

    private void OnDisable()
    {
        if (!_isInitialized) return;

        _inputManager.OnSelect -= Select;
    }

    private void Select()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        Vector2 mousePos = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
    
        if (hit.collider == null)
        {
            _currentSelectable?.Unselect();
            _currentSelectable = null;
            return;
        }

        if (!hit.collider.TryGetComponent<ISelectable>(out var selectable)) return;

        if (_currentSelectable == selectable) return;

        _currentSelectable?.Unselect();
        _currentSelectable = selectable;
        _currentSelectable.Select();
    }
}
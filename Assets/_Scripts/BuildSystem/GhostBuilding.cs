using UnityEngine;

namespace _Scripts.BuildSystem
{
    public class GhostBuilding : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Color _validColor = new Color(0, 1f, 0, 0.75f);
        private Color _invalidColor = new Color(1f, 0, 0, 0.75f);
    
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            SetValidPlacementColor(true);
        }
    
        public void SetValidPlacementColor(bool isValid)
        {
            _spriteRenderer.color = isValid ? _validColor : _invalidColor;
        }

        public void MoveGhost(Vector2 worldPosition)
        {
            transform.position = worldPosition;
        }
    }
}

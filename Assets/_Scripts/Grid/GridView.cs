using UnityEngine;

namespace _Scripts.Grid
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private int width = 20;
        [SerializeField] private int height = 20;
        [SerializeField] private float cellSize = 1f;

        private GridManager _gridManager;

        private void Awake()
        {
            _gridManager = new GridManager(width, height, cellSize);
        }

        private void OnDrawGizmos()
        {
            if (_gridManager == null) return;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var cell = _gridManager.GetCell(new Vector2Int(x, y));
                    Vector3 pos = new Vector3(x * cellSize + cellSize / 2f, y * cellSize + cellSize / 2f, 0f);
                    Gizmos.color = cell.IsWalkable ? Color.green : Color.red;
                    Gizmos.DrawWireCube(pos, Vector3.one * cellSize);
                }
            }
        }
    }
}
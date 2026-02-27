using _Scripts.Grid;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    private GridManager _gridManager;
    
    public void Init(GridManager gridManager)
    {
        _gridManager =  gridManager;
    }
    
    public void Build(Vector2Int gridOrigin, Vector2 worldCenter, BuildingSO buildingSO)
    {
        for (int x = 0; x < buildingSO.BuildingWidth; x++)
        {
            for (int y = 0; y < buildingSO.BuildingHeight; y++)
            {
                var cell = _gridManager.GetCell(gridOrigin + new Vector2Int(x, y));
                cell.IsOccupied = true;
            }
        }
        
        Instantiate(buildingSO.BuildingPrefab, worldCenter, Quaternion.identity);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "BuildingSO", menuName = "Scriptable Objects/BuildingSO")]
public class BuildingSO : ScriptableObject
{
    public int BuildingWidth => buildingWidth;
    [SerializeField] private int buildingWidth;
    
    public int BuildingHeight => buildingHeight;
    [SerializeField] private int buildingHeight;
    
    public GameObject BuildingPrefab => buildingPrefab;
    [SerializeField] private GameObject buildingPrefab;
}

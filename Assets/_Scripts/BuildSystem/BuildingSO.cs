using _Scripts.ResourcesSystem.Resources;
using UnityEngine;

namespace _Scripts.BuildSystem
{
    [CreateAssetMenu(fileName = "BuildingSO", menuName = "Scriptable Objects/BuildingSO")]
    public class BuildingSO : ScriptableObject
    {
        public int BuildingWidth => buildingWidth;
        [SerializeField] private int buildingWidth;
    
        public int BuildingHeight => buildingHeight;
        [SerializeField] private int buildingHeight;
    
        public GameObject BuildingPrefab => buildingPrefab;
        [SerializeField] private GameObject buildingPrefab;
    
        public ResourceStock[] BuildingCost => buildingCost;
        [SerializeField] private ResourceStock[] buildingCost;
    }
}

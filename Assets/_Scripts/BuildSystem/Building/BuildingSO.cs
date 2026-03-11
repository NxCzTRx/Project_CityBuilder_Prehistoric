using System.ComponentModel;
using _Scripts.ResourcesSystem.Resources;
using _Scripts.ResourcesSystem.Resources.ResourceTypes;
using UnityEngine;

namespace _Scripts.BuildSystem
{
    [CreateAssetMenu(fileName = "BuildingSO", menuName = "Scriptable Objects/BuildingSO")]
    public class BuildingSO : ScriptableObject
    {
        public string BuildingName => buildingName;
        [SerializeField] private string buildingName;
        
        public int BuildingWidth => buildingWidth;
        [SerializeField] private int buildingWidth;
    
        public int BuildingHeight => buildingHeight;
        [SerializeField] private int buildingHeight;
    
        public GameObject BuildingPrefab => buildingPrefab;
        [SerializeField] private GameObject buildingPrefab;
    
        public ResourceStock[] BuildingCost => buildingCost;
        [SerializeField] private ResourceStock[] buildingCost;

        public int MaxWorkers => maxWorkers;
        [SerializeField] private int maxWorkers;
        
        public float ProductionPerSecond => productionPerSecond;
        [SerializeField] private float productionPerSecond;

        public ResourceTypeSO ResourceProduction => resourceProduction;
        [SerializeField] private ResourceTypeSO resourceProduction;
        
        public int WorkCellFromGridOrigin => workCellFromGridOrigin;
        [Tooltip("Must be below width")]
        [SerializeField] private int workCellFromGridOrigin;
    }
}

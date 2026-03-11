using _Scripts.BuildSystem;
using _Scripts.ResourcesSystem.Resources.ResourceTypes;
using Mono.Cecil;
using UnityEngine;

[CreateAssetMenu(fileName = "WorkPlaceSO", menuName = "Scriptable Objects/BuildingSO/WorkPlaceSO")]
public class WorkPlaceSO : BuildingSO
{
    public int MaxWorkers => maxWorkers;
    [SerializeField] private int maxWorkers;
        
    public float ProductionPerSecond => productionPerSecond;
    [SerializeField] private float productionPerSecond;

    public ResourceTypeSO ResourceProduction => resourceProduction;
    [SerializeField] private ResourceTypeSO resourceProduction;
}

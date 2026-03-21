using UnityEngine;

namespace _Scripts.BuildSystem.InitialBuildings
{
    [CreateAssetMenu(menuName = "Scriptable Objects/InitialBuildingsSO")]
    public class InitialBuildingsSO : ScriptableObject
    {
        public InitialBuilding[] Buildings => buildings;
        [SerializeField] private InitialBuilding[] buildings;
    }
}

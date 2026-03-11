using UnityEngine;

namespace _Scripts.BuildSystem.Building.Housing
{
    [CreateAssetMenu(fileName = "HouseSO", menuName = "Scriptable Objects/BuildingSO/HouseSO")]
    public class HouseSO : BuildingSO
    {
        public int MaxResidents => maxResidents;
        [SerializeField] private int maxResidents;
    }
}

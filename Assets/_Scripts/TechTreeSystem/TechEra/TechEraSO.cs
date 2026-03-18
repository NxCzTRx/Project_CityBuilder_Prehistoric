using _Scripts.TechTreeSystem.TechNode;
using UnityEngine;

namespace _Scripts.TechTreeSystem.TechEra
{
    [CreateAssetMenu(fileName = "TechEraSO", menuName = "Scriptable Objects/Technology/TechEraSO")]
    public class TechEraSo : ScriptableObject
    {
        public string EraName => eraName;
        [SerializeField] private string eraName;

        public TechNodeSO[] TechNodes => techNodes;
        [SerializeField] private TechNodeSO[] techNodes;
    }
}

using _Scripts.ResourcesSystem.Resources.ResourceTypes;
using _Scripts.TechTreeSystem.TechEffect;
using UnityEngine;

namespace _Scripts.TechTreeSystem.TechNode
{
    [CreateAssetMenu(fileName = "TechNodeSO", menuName = "Scriptable Objects/Technology/TechNodeSO")]
    public class TechNodeSO : ScriptableObject
    {
        public string NodeName => nodeName;
        [SerializeField] private string nodeName;

        public Sprite Icon => icon;
        [SerializeField] private Sprite icon;

        public int TechCost => techCost;
        [SerializeField] private int techCost;
        
        public ResourceTypeSO KnowledgeResourceSo => knowledgeResourceSo;
        [SerializeField] private ResourceTypeSO knowledgeResourceSo;
        
        public TechEffectSO[] Effects => effects;
        [SerializeField] private TechEffectSO[] effects;
    }
}

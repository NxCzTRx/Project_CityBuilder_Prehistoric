using UnityEngine;

namespace _Scripts.ResourcesSystem.Resources.ResourceTypes
{
    [CreateAssetMenu(fileName = "ResourceTypeSO", menuName = "Scriptable Objects/ResourceTypeSO")]
    public class ResourceTypeSO : ScriptableObject
    {
        public string ResourceName;
        public Sprite ResourceIcon;
    }
}

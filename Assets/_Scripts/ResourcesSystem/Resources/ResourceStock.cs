using _Scripts.ResourcesSystem.Resources.ResourceTypes;

namespace _Scripts.ResourcesSystem.Resources
{
    [System.Serializable]
    public class ResourceStock
    {
        public ResourceTypeSO ResourceTypeSO;
        public float Amount;
        
        public ResourceStock(ResourceTypeSO resourceTypeSo, float amount)
        {
            ResourceTypeSO = resourceTypeSo;
            Amount = amount;
        }
    }
}

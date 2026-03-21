using _Scripts.NotificationSystem;
using _Scripts.ResourcesSystem;
using _Scripts.ResourcesSystem.Resources;
using UnityEngine;

namespace _Scripts.DisasterSystem.Disasters
{
    public class FoodRotDisaster : IDisaster
    {
        private readonly GameResourcesManager _gameResourcesManager;
        private readonly NotificationManager _notificationManager;
        
        private const string ResourceTypeName = "Food";
        private const float FoodStealRatio = 0.33f;
        
        public FoodRotDisaster(GameResourcesManager gameResourcesManager, NotificationManager notificationManager)
        {
            _gameResourcesManager = gameResourcesManager;
            _notificationManager = notificationManager;
        }
        
        public void OnStart()
        {
            var resourceTypeSo = _gameResourcesManager.GetResourceTypeByName(ResourceTypeName);

            var amount = _gameResourcesManager.GetAmount(resourceTypeSo);
            var amountToSubtract = amount * FoodStealRatio;
            
            _gameResourcesManager.RemoveResources(
                new ResourceStock(resourceTypeSo, amountToSubtract));
            
            _notificationManager.Notify($"{amountToSubtract} food has rotten.", 10f);
        }

        public void OnEnd()
        {
            
        }

        public float Duration { get; } = 0f;
    }
}

using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Roles;
using _Scripts.NotificationSystem;
using UnityEngine;

namespace _Scripts.DisasterSystem.Disasters
{
    public class BlizzardDisaster : IDisaster
    {
        private readonly RoleProductionRegistry _roleProductionRegistry;
        private readonly NotificationManager _notificationManager;

        private const float ProductionMultiplierDuringDisaster = 0.5f;

        private float _previousMultiplier;
        
        public BlizzardDisaster(RoleProductionRegistry roleProductionRegistry, NotificationManager notificationManager)
        {
            _roleProductionRegistry = roleProductionRegistry;
            _notificationManager = notificationManager;
        }
        
        public void OnStart()
        {
            _previousMultiplier = _roleProductionRegistry.BaseProductionMultiplier;
            _roleProductionRegistry.ApplyBaseBonus(ProductionMultiplierDuringDisaster - 1f);
            _notificationManager.Notify(
                "A blizzard has started, clan members will work more slowly", 10f);
        }

        public void OnEnd()
        {
            _roleProductionRegistry.ApplyBaseBonus(_previousMultiplier - _roleProductionRegistry.BaseProductionMultiplier);
            _notificationManager.Notify(
                "The blizzard has ended", 4f);
        }

        public float Duration { get; } = 100f;
    }
}

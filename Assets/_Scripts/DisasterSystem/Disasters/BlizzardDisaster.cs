using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Roles;
using UnityEngine;

namespace _Scripts.DisasterSystem.Disasters
{
    public class BlizzardDisaster : IDisaster
    {
        private readonly RoleProductionRegistry _roleProductionRegistry;

        private const float ProductionMultiplierDuringDisaster = 0.5f;

        private float _previousMultiplier;
        
        public BlizzardDisaster(RoleProductionRegistry roleProductionRegistry)
        {
            _roleProductionRegistry = roleProductionRegistry;
        }
        
        public void OnStart()
        {
            _previousMultiplier = _roleProductionRegistry.BaseProductionMultiplier;
            _roleProductionRegistry.ApplyBaseBonus(ProductionMultiplierDuringDisaster - 1f);
            Debug.Log("Blizzard DISASTER started");
        }

        public void OnEnd()
        {
            _roleProductionRegistry.ApplyBaseBonus(_previousMultiplier - _roleProductionRegistry.BaseProductionMultiplier);
            Debug.Log("Blizzard DISASTER ended");
        }

        public float Duration { get; } = 100f;
    }
}

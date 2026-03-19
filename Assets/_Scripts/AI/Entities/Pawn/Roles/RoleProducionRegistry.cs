using System.Collections.Generic;

namespace _Scripts.AI.Entities.Pawn.Roles
{
    public class RoleProductionRegistry
    {
        public float BaseProductionMultiplier => _baseProductionMultiplier;
        private float _baseProductionMultiplier = 1f;
        
        private readonly Dictionary<PawnRoleType, float> _multipliers = new()
        {
            { PawnRoleType.None,     1f },
            { PawnRoleType.Hunter,   1f },
            { PawnRoleType.Farmer, 1f },
            { PawnRoleType.Miner,  1f },
            { PawnRoleType.Lumberjack,  1f },
            { PawnRoleType.Shaman,  1f },
        };

        public float GetMultiplier(PawnRoleType role) =>
            _multipliers.TryGetValue(role, out var m) ? m * _baseProductionMultiplier : _baseProductionMultiplier;

        public void ApplyBonus(PawnRoleType role, float bonus) =>
            _multipliers[role] += bonus;

        public void ApplyBaseBonus(float bonus) =>
            _baseProductionMultiplier += bonus;
    }
}

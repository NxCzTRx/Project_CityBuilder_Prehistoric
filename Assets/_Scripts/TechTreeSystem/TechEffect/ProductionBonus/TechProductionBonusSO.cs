using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Roles;
using _Scripts.Core;
using UnityEngine;

namespace _Scripts.TechTreeSystem.TechEffect.ProductionBonus
{
    [CreateAssetMenu(fileName = "TechProductionEffect",
        menuName = "Scriptable Objects/Technology/TechEffect/TechProductionEffect")]
    public class TechProductionBonusSO : TechEffectSO
    {
        [SerializeField] private bool applyToAll;
        [SerializeField] private PawnRoleType pawnRoleType;
        [SerializeField] private float bonusToApply;

        public override void Apply(ObjectResolver resolver)
        {
            var registry = resolver.Resolve<RoleProductionRegistry>();
    
            if (applyToAll)
                registry.ApplyBaseBonus(bonusToApply);
            else
                registry.ApplyBonus(pawnRoleType, bonusToApply);
        }
    }
}

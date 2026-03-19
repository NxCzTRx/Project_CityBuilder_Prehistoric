using _Scripts.Core;
using _Scripts.ImmigrationSystem;
using UnityEngine;

namespace _Scripts.TechTreeSystem.TechEffect.InhabitantsLimit
{
    [CreateAssetMenu(fileName = "InhabitantsLimitEffect",
        menuName = "Scriptable Objects/Technology/TechEffect/InhabitantsLimitEffect")]
    public class TechInhabitantsIncreaseSO : TechEffectSO
    { 
        [SerializeField] private int maxInhabitantsIncrease;

        public override void Apply(ObjectResolver resolver)
        {
            resolver.Resolve<ImmigrationManager>().SetMaxInhabitants(maxInhabitantsIncrease);
        }
    }
}

using _Scripts.BuildSystem;
using _Scripts.Core;
using _Scripts.UI.Gameplay;
using UnityEngine;

namespace _Scripts.TechTreeSystem.TechEffect.NewBuilding
{
    [CreateAssetMenu(fileName = "NewBuildingEffect",
        menuName = "Scriptable Objects/Technology/TechEffect/NewBuildingEffect")]
    public class TechNewBuildingSO : TechEffectSO
    {
        [SerializeField] private BuildingSO buildingSO;

        public override void Apply(ObjectResolver resolver)
        {
            resolver.Resolve<GameplayUI>().BuildUI.AddNewBuilding(buildingSO);
        }
    }
}
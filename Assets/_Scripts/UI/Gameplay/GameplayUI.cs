using _Scripts.Core;
using _Scripts.ResourcesSystem;
using _Scripts.TechTreeSystem;
using _Scripts.UI.Gameplay.Build;
using _Scripts.UI.Gameplay.TechnologyTree;
using UnityEngine;

namespace _Scripts.UI.Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        public BuildUI BuildUI => buildUI;
        [SerializeField] private BuildUI buildUI;
        
        [SerializeField] private PlayerResourcesUI playerResourcesUI;
        [SerializeReference] private TechnologyTreeUI technologyTreeUI;
    
        public void Init(ObjectResolver objectResolver)
        {
            playerResourcesUI.Init(objectResolver.Resolve<GameResourcesManager>());
            technologyTreeUI.Init(objectResolver.Resolve<TechTreeManager>());
        }
    }
}

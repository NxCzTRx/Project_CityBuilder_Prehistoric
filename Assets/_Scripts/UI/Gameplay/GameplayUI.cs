using _Scripts.Core;
using _Scripts.ResourcesSystem;
using _Scripts.TechTreeSystem;
using _Scripts.UI.Gameplay.TechnologyTree;
using UnityEngine;

namespace _Scripts.UI.Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private PlayerResourcesUI playerResourcesUI;
        [SerializeReference] private TechnologyTreeUI technologyTreeUI;
    
        public void Init(ObjectResolver objectResolver)
        {
            playerResourcesUI.Init(objectResolver.Resolve<GameResourcesManager>());
            technologyTreeUI.Init(objectResolver.Resolve<TechTreeManager>());
        }
    }
}

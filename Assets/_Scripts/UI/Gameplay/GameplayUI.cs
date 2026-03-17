using _Scripts.ResourcesSystem;
using UnityEngine;

namespace _Scripts.UI.Gameplay
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private PlayerResourcesUI playerResourcesUI;
    
        public void Init(GameResourcesManager gameResourcesManager)
        {
            playerResourcesUI.Init(gameResourcesManager);
        }
    }
}

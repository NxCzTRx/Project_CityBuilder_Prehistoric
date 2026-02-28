using _Scripts.BuildSystem;
using _Scripts.Input;
using UnityEngine;

namespace _Scripts.Core.GameMode.Modes
{
    public class BuildGameMode : IGameMode
    {
        private GameModeManager _myGameModeManager;
        
        private readonly InputManager _inputManager;
        private readonly BuildManager _buildManager;

        public BuildGameMode(InputManager inputManager, BuildManager buildManager)
        {
            _inputManager = inputManager;
            _buildManager = buildManager;
        }

        public void Enter(GameModeManager gameModeManager)
        {
            _myGameModeManager = gameModeManager;
            
            _inputManager.ChangeCurrentScheme("Build");
            _buildManager.SetBuildManagerActive(true);
        }

        public void Exit()
        {
            _buildManager.SetBuildManagerActive(false);
        }
    }
}

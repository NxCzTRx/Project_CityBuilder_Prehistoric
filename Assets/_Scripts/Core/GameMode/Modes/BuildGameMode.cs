using _Scripts.BuildSystem;
using _Scripts.Input;
using UnityEngine;

namespace _Scripts.Core.GameMode.Modes
{
    public class BuildGameMode : IGameMode
    {
        private BuildingSO _buildingSo;
        
        private GameModeManager _myGameModeManager;

        public BuildGameMode(BuildingSO buildingSo)
        {
            _buildingSo = buildingSo;
        }

        public void Enter(GameModeManager gameModeManager)
        {
            _myGameModeManager = gameModeManager;
            
            _myGameModeManager.InputManager.ChangeCurrentScheme("Build");
            _myGameModeManager.BuildManager.EnableBuildManager(_buildingSo);
            _myGameModeManager.InputManager.OnExitConstructionMode += QuitConstruction;
        }

        private void QuitConstruction()
        {
            _myGameModeManager.ChangeGameMode(new DefaultGameMode());
        }

        public void Exit()
        {
            _myGameModeManager.InputManager.OnExitConstructionMode -= QuitConstruction;
            _myGameModeManager.BuildManager.DisableBuildManager();
        }
    }
}

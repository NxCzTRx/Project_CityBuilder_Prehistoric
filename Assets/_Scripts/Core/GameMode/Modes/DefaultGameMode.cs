using _Scripts.Input;

namespace _Scripts.Core.GameMode.Modes
{
    public class DefaultGameMode : IGameMode
    {
        private GameModeManager _myGameModeManager;
        
        private readonly InputManager _inputManager;
        
        public void Enter(GameModeManager gameModeManager)
        {
            _myGameModeManager = gameModeManager;
            
            _myGameModeManager.InputManager.ChangeCurrentScheme("Default");
        }

        public void Exit()
        {
        
        }
    }
}

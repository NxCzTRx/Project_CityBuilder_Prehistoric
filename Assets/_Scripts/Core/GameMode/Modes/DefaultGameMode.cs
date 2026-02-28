using _Scripts.Input;

namespace _Scripts.Core.GameMode.Modes
{
    public class DefaultGameMode : IGameMode
    {
        private GameModeManager _myGameModeManager;
        
        private readonly InputManager _inputManager;

        public DefaultGameMode(InputManager inputManager)
        {
            _inputManager = inputManager;
        }
        
        public void Enter(GameModeManager gameModeManager)
        {
            _myGameModeManager = gameModeManager;
            
            _inputManager.ChangeCurrentScheme("Default");
        }

        public void Exit()
        {
        
        }
    }
}

using _Scripts.Input;

namespace _Scripts.Core.GameMode
{
    public class GameModeManager
    {
        private IGameMode _currentGameMode;
        public IGameMode CurrentGameMode => _currentGameMode;
        
        public GameModeManager(IGameMode initialGameMode, InputManager inputManager)
        {
            _currentGameMode = initialGameMode;
        }
        
        public void ChangeGameMode(IGameMode newGameMode)
        {
            _currentGameMode?.Exit();
            _currentGameMode = newGameMode;
            _currentGameMode.Enter(this);
        }
    }
}

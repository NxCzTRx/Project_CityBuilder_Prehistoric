using System;
using _Scripts.BuildSystem;
using _Scripts.Core.GameMode.Modes;
using _Scripts.Events;
using _Scripts.Input;

namespace _Scripts.Core.GameMode
{
    public class GameModeManager : IDisposable
    {
        private IGameMode _currentGameMode;
        public IGameMode CurrentGameMode => _currentGameMode;
        
        public InputManager InputManager {get; private set;}
        public BuildManager BuildManager {get; private set;}
        
        public GameModeManager(IGameMode initialGameMode, InputManager inputManager, BuildManager buildManager)
        {
            _currentGameMode = initialGameMode;
            InputManager = inputManager;
            BuildManager = buildManager;
            
            EventBus<OnBuildingSelected>.Subscribe(HandleOnBuildingSelected);
        }

        private void HandleOnBuildingSelected(OnBuildingSelected st)
        {
            ChangeGameMode(new BuildGameMode(st.BuildingSo));
        }

        public void ChangeGameMode(IGameMode newGameMode)
        {
            _currentGameMode?.Exit();
            _currentGameMode = newGameMode;
            _currentGameMode.Enter(this);
        }

        public void Dispose()
        {
            _currentGameMode?.Exit();
            EventBus<OnBuildingSelected>.Unsubscribe(HandleOnBuildingSelected);
        }
    }
}

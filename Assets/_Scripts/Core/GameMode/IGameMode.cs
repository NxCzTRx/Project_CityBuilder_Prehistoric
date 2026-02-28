namespace _Scripts.Core.GameMode
{
    public interface IGameMode
    {
        void Enter(GameModeManager gameModeManager);
        void Exit();
    }
}

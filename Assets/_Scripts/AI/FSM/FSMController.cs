using _Scripts.AI.FSM.States;

namespace _Scripts.AI.FSM
{
    public abstract class FSMController<T>
    {
        private State<T> _currentState;
    
        public virtual void Tick()
        {
            _currentState?.OnUpdate();
        }
    
        public void ChangeState(State<T> newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }
    }
}

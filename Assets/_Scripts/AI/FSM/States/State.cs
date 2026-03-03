namespace _Scripts.AI.FSM.States
{
    public abstract class State<T>
    {
        protected T Controller;
        
        public State(T controller)
        {
            this.Controller = controller;
        }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();
    }
}

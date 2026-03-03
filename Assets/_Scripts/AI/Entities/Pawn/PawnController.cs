using _Scripts.AI.FSM;
using _Scripts.AI.FSM.States;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnController : FSMController<PawnController>
    {
        public PawnModel Model { get; }
        public PawnView View { get; }

        public PawnController(PawnModel model, PawnView view)
        {
            Model = model;
            View = view;
        }
    }
}

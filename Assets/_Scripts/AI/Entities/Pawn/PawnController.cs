using _Scripts.AI.FSM;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnController : FSMController<PawnController>
    {
        public PawnModel Model { get; }
        public PawnView View { get; }
        
        private float hungerDecayRate = 0.5f;
        private float healthDecayRate = 0.75f;

        public PawnController(PawnModel model, PawnView view)
        {
            Model = model;
            View = view;
            
            View.Init(model);
        }

        public override void Tick()
        {
            base.Tick();

            UpdateNeeds();
        }
        
        private void UpdateNeeds()
        {
            if (Model.Hunger > 0) 
                Model.Hunger -= hungerDecayRate * Time.deltaTime;
            else
                Model.Health -= healthDecayRate * Time.deltaTime;
        }
    }
}

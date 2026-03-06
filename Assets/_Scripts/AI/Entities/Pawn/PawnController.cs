using _Scripts.AI.FSM;
using _Scripts.AI.FSM.States;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnController : FSMController<PawnController>
    {
        private PawnEntity _entity;
        public PawnModel Model { get; }
        public PawnView View { get; }
        public PawnSpawner MySpawner { get; }

        public PawnController(PawnModel model, PawnView view, PawnEntity entity ,PawnSpawner spawner)
        {
            Model = model;
            View = view;
            MySpawner = spawner;
            _entity = entity;
            
            View.Init(model);
            ChangeState(new PawnMoveTo(this, Model.GridManager.GetCell(
                GridUtils.WorldToGridPosition(Vector3.zero, Model.GridManager.CellSize)))); //ASSIGN MAIN BASE (HOUSE)

            Model.OnDie += Die;
        }

        public override void Tick()
        {
            base.Tick();

            UpdateNeeds();
        }
        
        private void UpdateNeeds()
        {
            if (Model.Hunger > 0) 
                Model.Hunger -= Model.HungerDecayRate * Time.deltaTime;
            else
                Model.Health -= Model.HealthDecayRate * Time.deltaTime;
        }

        private void Die()
        {
            MySpawner.Despawn(_entity);
        }
    }
}

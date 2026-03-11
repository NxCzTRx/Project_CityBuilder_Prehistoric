using _Scripts.AI.FSM;
using _Scripts.AI.FSM.States;
using _Scripts.Core;
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
        public ObjectResolver Resolver { get; }

        public PawnController(
            PawnModel model, 
            PawnView view, 
            PawnEntity entity ,
            PawnSpawner spawner, 
            ObjectResolver resolver)
        {
            Model = model;
            View = view;
            MySpawner = spawner;
            _entity = entity;
            Resolver = resolver;
            
            View.Init(model); 
            
            //ASSIGN MAIN BASE (HOUSE)

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

using System;
using _Scripts.AI.Entities.Pawn.Scheduling;
using _Scripts.AI.FSM;
using _Scripts.AI.FSM.States;
using _Scripts.Core;
using _Scripts.Grid;
using _Scripts.ResourcesSystem;
using _Scripts.ResourcesSystem.Resources.ResourceTypes;
using Mono.Cecil;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnController : FSMController<PawnController>, IDisposable
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
            {
                Model.Hunger -= Model.HungerDecayRate * Time.deltaTime;
                Model.Health += Model.HealthDecayRate * Time.deltaTime;
            }
            else
                Model.Health -= Model.HealthDecayRate * Time.deltaTime;

            if (Model.Hunger < Model.HungerThreshold && !Model.IsHandlingUrgentState)
            {
                Model.IsHandlingUrgentState = true;
                ChangeState(new PawnEatState(this, Model.HouseController.Model.EntranceCell));
            }
        }

        private void Die()
        {
            MySpawner.Despawn(_entity);
        }

        public void Dispose()
        {
            Model.OnDie -= Die;
        }
    }
}

using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Roles;
using _Scripts.AI.Entities.Pawn.Scheduling;
using _Scripts.AI.FSM.States;
using _Scripts.Core;
using _Scripts.ResourcesSystem.Resources;
using UnityEngine;

namespace _Scripts.BuildSystem.Building.WorkPlace
{
    public class WorkPlaceController
    {
        public WorkPlaceModel Model { get; }
        public WorkPlaceView View { get; }
        
        private readonly PawnScheduler _pawnScheduler;
        private readonly RoleProductionRegistry _roleProductionRegistry;

        public WorkPlaceController(WorkPlaceModel model, WorkPlaceView view, ObjectResolver objectResolver)
        {
            _pawnScheduler = objectResolver.Resolve<PawnScheduler>();
            _roleProductionRegistry = objectResolver.Resolve<RoleProductionRegistry>();
            
            Model = model;
            View = view;

            View.Init(this, objectResolver.Resolve<PawnRegistry>());
        }

        public bool HasSpace => Model.PawnWorkers.Count < Model.WorkPlaceSO.MaxWorkers;

        public void AssignWorker(PawnController pawnController)
        {
            if (!HasSpace) return;
            Model.PawnWorkers.Add(pawnController);
            pawnController.Model.CurrentRole = Model.WorkPlaceSO.PawnRoleType;
            pawnController.Model.WorkPlaceController = this;
            _pawnScheduler.EvaluatePawn(pawnController);
            
            if (Model.WorkPlaceSO.PawnRoleType == PawnRoleType.None)
                Debug.LogError("Pawn role cannot be None, check building pawnRoleType");
        }

        public void RemoveWorker(PawnController pawnController)
        {
            if (Model.PawnWorkers.Count == 0) return;
            Model.PawnWorkers.Remove(pawnController);
            pawnController.ChangeState(new PawnIdleState(pawnController));
            
            pawnController.Model.CurrentRole = PawnRoleType.None;
            pawnController.Model.WorkPlaceController = null;
        }
        
        public void RemoveRandomWorker()
        {
            if (Model.PawnWorkers.Count == 0) return;
    
            var randomWorker = Model.PawnWorkers[Random.Range(0, Model.PawnWorkers.Count)];
            RemoveWorker(randomWorker);
        }

        public ResourceStock GetProduction(float deltaSecondsProducing)
        {
            var roleMultiplier = _roleProductionRegistry.GetMultiplier(Model.WorkPlaceSO.PawnRoleType);
            var amount = Model.WorkPlaceSO.ProductionPerSecond * roleMultiplier * deltaSecondsProducing;
    
            return new ResourceStock(Model.WorkPlaceSO.ResourceProduction, amount);
        }
    }
}

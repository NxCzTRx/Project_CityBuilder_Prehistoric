using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Roles;
using _Scripts.AI.Entities.Pawn.Scheduling;
using _Scripts.AI.FSM.States;
using _Scripts.Core;
using _Scripts.ResourcesSystem.Resources;

namespace _Scripts.BuildSystem.Building.WorkPlace
{
    public class WorkPlaceController
    {
        public WorkPlaceModel Model { get; }
        public WorkPlaceView View { get; }
        
        private readonly ObjectResolver _objectResolver;
        private readonly PawnScheduler _pawnScheduler;

        public WorkPlaceController(WorkPlaceModel model, WorkPlaceView view, ObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
            _pawnScheduler = _objectResolver.Resolve<PawnScheduler>();
            
            Model = model;
            View = view;

            View.Init(this, _objectResolver.Resolve<PawnRegistry>());
        }

        public bool HasSpace => Model.PawnWorkers.Count < Model.WorkPlaceSO.MaxWorkers;

        public void AssignWorker(PawnController pawnController)
        {
            if (!HasSpace) return;
            Model.PawnWorkers.Push(pawnController);
            pawnController.Model.CurrentRole = PawnRoleType.Employee;
            pawnController.Model.WorkPlaceController = this;
            _pawnScheduler.EvaluatePawn(pawnController);
        }

        public void RemoveWorker()
        {
            if (Model.PawnWorkers.Count == 0) return;
            var pawn = Model.PawnWorkers.Pop();
            pawn.ChangeState(new PawnIdleState(pawn));
            
            pawn.Model.CurrentRole = PawnRoleType.None;
            pawn.Model.WorkPlaceController = null;
        }

        public ResourceStock GetProduction(float deltaSecondsProducing)
        {
            return new ResourceStock(Model.WorkPlaceSO.ResourceProduction, 
                Model.WorkPlaceSO.ProductionPerSecond * deltaSecondsProducing);
        }
    }
}

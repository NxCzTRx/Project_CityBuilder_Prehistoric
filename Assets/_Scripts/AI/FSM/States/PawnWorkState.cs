using _Scripts.AI.Entities.Pawn;
using _Scripts.BuildSystem.Building;
using _Scripts.BuildSystem.Building.WorkPlace;
using _Scripts.ResourcesSystem;
using UnityEngine;

namespace _Scripts.AI.FSM.States
{
    public class PawnWorkState : State<PawnController>
    {
        private WorkPlaceController _workPlaceController;
        private GameResourcesManager _gameResourcesManager;
    
        public PawnWorkState(
            PawnController controller, 
            WorkPlaceController workPlaceController, 
            GameResourcesManager gameResourcesManager)
            : base(controller)
        {
            Controller = controller;
            _workPlaceController = workPlaceController;
            _gameResourcesManager = gameResourcesManager;
        }

        public override void OnEnter()
        {
        
        }

        public override void OnUpdate()
        {
            var stock = _workPlaceController.GetProduction(Time.deltaTime);
            _gameResourcesManager.AddResources(stock);
        }

        public override void OnExit()
        {

        }
    }
}

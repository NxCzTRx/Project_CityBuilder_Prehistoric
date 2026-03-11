using System.Numerics;
using _Scripts.AI.Entities.Pawn;
using _Scripts.Grid;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Scripts.AI.FSM.States
{
    public class PawnIdleState : State<PawnController>
    {
        //Test counter for idle state
        private float _idleTime;
        
        private GridManager _gridManager;
        
        public PawnIdleState(PawnController controller) : base(controller)
        {
            Controller = controller;

            _gridManager = Controller.Resolver.Resolve<GridManager>();
        }

        public override void OnEnter()
        {
            _idleTime = 0;
        }

        public override void OnUpdate()
        {
            //TEST
            _idleTime += Time.deltaTime;
            
            if (_idleTime > 2f)
            {
                // After idling for 2 seconds, move to a random position
                Controller.ChangeState(new PawnMoveTo(Controller,
                    _gridManager.GetCell(new Vector2Int(Random.Range(0, 20), Random.Range(0, 20)))));
            }
        }

        public override void OnExit()
        {
        }
    }
}

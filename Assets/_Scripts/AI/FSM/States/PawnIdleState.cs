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
        public PawnIdleState(PawnController controller) : base(controller)
        {
            Controller = controller;
        }

        public override void OnEnter()
        {
            _idleTime = 0;
        }

        public override void OnUpdate()
        {
            //TEST
            _idleTime += UnityEngine.Time.deltaTime;
            
            if (_idleTime > 2f)
            {
                // After idling for 2 seconds, move to a random position
                Controller.ChangeState(new PawnMoveTo(Controller,
                    Controller.Model.GridManager.GetCell(new Vector2Int(5, 5))));
            }
        }

        public override void OnExit()
        {
        }
    }
}

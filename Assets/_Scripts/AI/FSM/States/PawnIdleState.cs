using _Scripts.AI.Entities.Pawn;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.FSM.States
{
    public class PawnIdleState : State<PawnController>
    {
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
            _idleTime += Time.deltaTime;
            
            if (_idleTime > 2f)
            {
                var current = Controller.Model.CurrentCell.Position;
                var offset = new Vector2Int(Random.Range(-3, 4), Random.Range(-3, 4));
                var target = new Vector2Int(
                    Mathf.Clamp(current.x + offset.x, 0, _gridManager.GridWidth - 1),
                    Mathf.Clamp(current.y + offset.y, 0, _gridManager.GridHeight - 1));

                Controller.ChangeState(new PawnMoveTo(Controller, _gridManager.GetCell(target)));
            }
        }

        public override void OnExit()
        {
        }
    }
}
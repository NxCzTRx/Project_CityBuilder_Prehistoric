using System.Collections.Generic;
using System.Linq;
using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Pathfinding;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.FSM.States
{
    public class PawnMoveTo : State<PawnController>
    {
        private Cell _targetCell;
        private Queue<Vector3> _path;
        
        public PawnMoveTo(PawnController controller, Cell targetCell) : base(controller)
        {
            Controller = controller;
            _targetCell = targetCell;
        }

        public override void OnEnter()
        {
            var gridPositions = AStar.FindPath(
                Controller.Model.CurrentCell,
                _targetCell, 
                Controller.Model.GridManager);
            
                _path = new Queue<Vector3>(
                    gridPositions.Select(p => GridUtils.CellToWorldPosition(
                        p, Controller.Model.GridManager.CellSize)));
        }

        public override void OnUpdate()
        {
            if (_path.Count == 0)
            {
                Controller.ChangeState(new PawnIdleState(Controller));
                return;
            }

            var next = _path.Peek();
            Controller.View.MoveTowards(next, Controller.Model.Speed);

            if (Controller.View.HasReached(next))
            {
                Controller.View.transform.position = next;
                
                _path.Dequeue();
                Controller.Model.CurrentCell = Controller.Model.GridManager.GetCell(
                    GridUtils.WorldToGridPosition(next, Controller.Model.GridManager.CellSize));
            }
        }

        public override void OnExit()
        {
            
        }
    }
}

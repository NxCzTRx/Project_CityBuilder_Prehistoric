using System.Collections.Generic;
using System.Linq;
using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.FSM.States;
using _Scripts.AI.Pathfinding;
using _Scripts.Grid;
using _Scripts.ResourcesSystem;
using Unity.VisualScripting;
using UnityEngine;

public class PawnMoveToHouse : State<PawnController>
{
    private Cell _targetCell;
    private Queue<Vector3> _path;
    
    private GridManager _gridManager;
        
    public PawnMoveToHouse(PawnController controller, Cell targetCell) : base(controller)
    {
        Controller = controller;
        _targetCell = targetCell;
        
        _gridManager = Controller.Resolver.Resolve<GridManager>();
    }

    public override void OnEnter()
    {
        var gridPositions = AStar.FindPath(
            Controller.Model.CurrentCell,
            _targetCell, 
            _gridManager);

        _path = new Queue<Vector3>(
            gridPositions.Select(p => GridUtils.CellToWorldPosition(
                p, _gridManager.CellSize)));
    }

    public override void OnUpdate()
    {
        if (Controller.Model.CurrentCell == _targetCell) return;

        if (_path.Count == 0) return;

        var next = _path.Peek();
        Controller.View.MoveTowards(next, Controller.Model.Speed);

        if (!Controller.View.HasReached(next)) return;
        
        Controller.View.transform.position = next;
                
        _path.Dequeue();
        Controller.Model.CurrentCell = _gridManager.GetCell(
            GridUtils.WorldToGridPosition(next, _gridManager.CellSize));
    }

    public override void OnExit()
    {
            
    }
}

using System.Collections.Generic;
using System.Linq;
using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Scheduling;
using _Scripts.AI.FSM.States;
using _Scripts.AI.Pathfinding;
using _Scripts.Grid;
using _Scripts.ResourcesSystem;
using _Scripts.ResourcesSystem.Resources;
using UnityEngine;

public class PawnEatState : State<PawnController>
{
    private Cell _targetCell;
    private Queue<Vector3> _path;
        
    private GridManager _gridManager;
    private PawnScheduler _scheduler;
    private GameResourcesManager _gameResourcesManager;
    
    public PawnEatState(PawnController controller, Cell targetCell) : base(controller)
    {
        _targetCell = targetCell;
            
        _gridManager = Controller.Resolver.Resolve<GridManager>();
        _scheduler = Controller.Resolver.Resolve<PawnScheduler>();
        _gameResourcesManager = Controller.Resolver.Resolve<GameResourcesManager>();
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
        if (_path.Count == 0)
        {
            Controller.Model.IsHandlingUrgentState = false;
            _scheduler.EvaluatePawn(Controller);
            return;
        }

        var next = _path.Peek();
        Controller.View.MoveTowards(next, Controller.Model.Speed);

        if (Controller.View.HasReached(next))
        {
            Controller.View.transform.position = next;
                
            _path.Dequeue();
            Controller.Model.CurrentCell = _gridManager.GetCell(
                GridUtils.WorldToGridPosition(next, _gridManager.CellSize));
        }
    }

    public override void OnExit()
    {
        Controller.Model.Hunger = Controller.Model.MaxHunger;
        
        if (_gameResourcesManager.GetAmount(Controller.Model.WhatIsFood) > 0)
            _gameResourcesManager.RemoveResources(new ResourceStock(Controller.Model.WhatIsFood, 1));
    }
}

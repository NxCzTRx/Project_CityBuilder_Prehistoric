using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.FSM.States;
using _Scripts.BuildSystem.Building;
using _Scripts.ResourcesSystem;
using UnityEngine;

public class PawnWorkState : State<PawnController>
{
    private BuildingController _buildingController;
    private GameResourcesManager _gameResourcesManager;
    
    public PawnWorkState(
        PawnController controller, 
        BuildingController buildingController, 
        GameResourcesManager gameResourcesManager)
        : base(controller)
    {
        Controller = controller;
        _buildingController = buildingController;
        _gameResourcesManager = gameResourcesManager;
    }

    public override void OnEnter()
    {
        
    }

    public override void OnUpdate()
    {
        var stock = _buildingController.GetProduction(Time.deltaTime);
        _gameResourcesManager.AddResources(stock);
    }

    public override void OnExit()
    {

    }
}

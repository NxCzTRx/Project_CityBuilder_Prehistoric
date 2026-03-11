using System.Collections.Generic;
using _Scripts.AI.Entities.Pawn;
using _Scripts.Grid;

namespace _Scripts.BuildSystem.Building.WorkPlace
{
    public class WorkPlaceModel
    {
        public WorkPlaceSO WorkPlaceSO { get; }
        public Stack<PawnController> PawnWorkers { get; }
        public Cell WorkCell;


        public WorkPlaceModel(WorkPlaceSO workPlaceSO, Cell workCell)
        {
            WorkPlaceSO = workPlaceSO;
            PawnWorkers = new Stack<PawnController>(workPlaceSO.MaxWorkers);
            
            WorkCell = workCell;
        }
    }
}

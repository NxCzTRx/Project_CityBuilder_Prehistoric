using System.Collections.Generic;
using _Scripts.AI.Entities.Pawn;
using _Scripts.Grid;
using NUnit.Framework;

namespace _Scripts.BuildSystem.Building.WorkPlace
{
    public class WorkPlaceModel
    {
        public WorkPlaceSO WorkPlaceSO { get; }
        public List<PawnController> PawnWorkers { get; }
        public Cell WorkCell;


        public WorkPlaceModel(WorkPlaceSO workPlaceSO, Cell workCell)
        {
            WorkPlaceSO = workPlaceSO;
            PawnWorkers = new List<PawnController>(workPlaceSO.MaxWorkers);
            
            WorkCell = workCell;
        }
    }
}

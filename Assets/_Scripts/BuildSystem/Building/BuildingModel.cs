using System.Collections;
using System.Collections.Generic;
using _Scripts.AI.Entities.Pawn;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.BuildSystem.Building
{
    public class BuildingModel
    {
        public BuildingSO BuildingSO { get; }
        public Stack<PawnController> PawnWorkers { get; }
        public Cell WorkCell;


        public BuildingModel(BuildingSO buildingSO, Cell workCell)
        {
            BuildingSO = buildingSO;
            PawnWorkers = new Stack<PawnController>(buildingSO.MaxWorkers);
            
            WorkCell = workCell;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using _Scripts.AI.Entities.Pawn;

namespace _Scripts.BuildSystem.Building
{
    public class BuildingModel
    {
        public BuildingSO BuildingSO { get; }

        public Stack<PawnController> PawnWorkers { get; }


        public BuildingModel(BuildingSO buildingSO)
        {
            this.BuildingSO = buildingSO;
            PawnWorkers = new Stack<PawnController>(buildingSO.MaxWorkers);
        }
    }
}

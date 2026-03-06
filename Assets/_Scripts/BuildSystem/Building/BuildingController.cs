using _Scripts.AI.Entities.Pawn;

namespace _Scripts.BuildSystem.Building
{
    public class BuildingController
    {
        public BuildingModel Model { get; }
        public BuildingView View { get; }

        public BuildingController(BuildingModel model, BuildingView view)
        {
            Model = model;
            View = view;
        }

        public bool HasSpace => Model.PawnWorkers.Count < Model.BuildingSO.MaxWorkers;

        public void AssignWorker(PawnController pawnController)
        {
            if (!HasSpace) return;
            Model.PawnWorkers.Push(pawnController);
        }

        public void RemoveWorker()
        {
            if (Model.PawnWorkers.Count == 0) return;
            Model.PawnWorkers.Pop();
        }
    }
}

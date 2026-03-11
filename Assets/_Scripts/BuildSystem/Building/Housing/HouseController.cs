
using _Scripts.AI.Entities.Pawn;

namespace _Scripts.BuildSystem.Building.Housing
{
    public class HouseController
    {
        public HouseModel Model { get; }
        public HouseView View { get; }

        public HouseController(HouseModel model, HouseView view)
        {
            Model = model;
            View = view;
        }

        public bool HasSpace => Model.PawnResidents.Count < Model.HouseSO.MaxResidents;

        public void AssignResident(PawnController pawn)
        {
            if (!HasSpace) return;
            Model.PawnResidents.Add(pawn);
            pawn.Model.HouseController = this;
        }

        public void RemoveResident(PawnController pawn)
        {
            if (Model.PawnResidents.Count == 0) return;
            Model.PawnResidents.Remove(pawn);
            pawn.Model.HouseController = null;
        }
    }
}


using _Scripts.AI.Entities.Pawn;

namespace _Scripts.BuildSystem.Building.Housing
{
    public class HouseController
    {
        public HouseModel Model { get; }
        public HouseView View { get; }
        
        private HousingRegistry _housingRegistry;

        public HouseController(HouseModel model, HouseView view, HousingRegistry housingRegistry)
        {
            Model = model;
            View = view;
            
            _housingRegistry = housingRegistry;
        }

        public bool HasSpace => Model.PawnResidents.Count < Model.HouseSO.MaxResidents;

        public void AssignResident(PawnController pawn)
        {
            if (!HasSpace) return;
            Model.PawnResidents.Add(pawn);
            pawn.Model.HouseController = this;
            
            _housingRegistry?.AddOccupant(1);
        }

        public void RemoveResident(PawnController pawn)
        {
            if (Model.PawnResidents.Count == 0) return;
            Model.PawnResidents.Remove(pawn);
            pawn.Model.HouseController = null;
            
            _housingRegistry?.RemoveOccupant(1);
        }
    }
}

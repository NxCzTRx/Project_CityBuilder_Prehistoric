using System.Collections.Generic;
using System.Linq;

namespace _Scripts.BuildSystem.Building.Housing
{
    public class HousingRegistry
    {
        private readonly List<HouseController> _houses = new();

        public void RegisterHouse(HouseController house)
        {
            _houses.Add(house);
        }

        public void UnregisterHouse(HouseController house)
        {
            _houses.Remove(house);
        }

        public bool HasAvailableHousing => 
            _houses.Any(h => h.HasSpace);

        public int TotalCapacity => 
            _houses.Sum(h => h.Model.HouseSO.MaxResidents);

        public int TotalOccupied => 
            _houses.Sum(h => h.Model.PawnResidents.Count);

        public HouseController GetAvailableHouse() =>
            _houses.FirstOrDefault(h => h.HasSpace);

        public IReadOnlyList<HouseController> GetAllHouses() => _houses;
    }
}

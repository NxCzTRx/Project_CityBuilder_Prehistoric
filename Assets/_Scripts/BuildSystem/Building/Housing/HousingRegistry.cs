using System.Collections.Generic;
using System.Linq;
using _Scripts.Events;

namespace _Scripts.BuildSystem.Building.Housing
{
    public class HousingRegistry
    {
        private readonly List<HouseController> _houses = new();

        private int _occupiedSpace = 0;
        private int _maxSpace = 0;

        public void RegisterHouse(HouseController house)
        {
            _houses.Add(house);
            _maxSpace += house.Model.HouseSO.MaxResidents;
            
            EventBus<OnHousingUpdated>.Publish(new OnHousingUpdated(_maxSpace, _occupiedSpace));
        }

        public void UnregisterHouse(HouseController house)
        {
            _houses.Remove(house);
            _maxSpace -= house.Model.HouseSO.MaxResidents;
            
            EventBus<OnHousingUpdated>.Publish(new OnHousingUpdated(_maxSpace, _occupiedSpace));
        }

        public void AddOccupant(int quantity)
        {
            _occupiedSpace += quantity;
            
            EventBus<OnHousingUpdated>.Publish(new OnHousingUpdated(_maxSpace, _occupiedSpace));
        } 
        
        public void RemoveOccupant(int quantity)
        {
            _occupiedSpace -= quantity;
            
            EventBus<OnHousingUpdated>.Publish(new OnHousingUpdated(_maxSpace, _occupiedSpace));
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

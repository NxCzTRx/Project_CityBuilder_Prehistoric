using System.Collections.Generic;
using System.Linq;
using _Scripts.Events;
using UnityEngine;

namespace _Scripts.BuildSystem.Building.Housing
{
    public class HousingRegistry
    {
        private readonly List<HouseController> _houses = new();

        public int OccupiedSpace { get; private set; } = 0;
        public int MaxSpace { get; private set; } = 0;

        public void RegisterHouse(HouseController house)
        {
            _houses.Add(house);
            MaxSpace += house.Model.HouseSO.MaxResidents;
            
            EventBus<OnHousingUpdated>.Publish(new OnHousingUpdated(MaxSpace, OccupiedSpace));
        }

        public void UnregisterHouse(HouseController house)
        {
            _houses.Remove(house);
            MaxSpace -= house.Model.HouseSO.MaxResidents;
            
            EventBus<OnHousingUpdated>.Publish(new OnHousingUpdated(MaxSpace, OccupiedSpace));
        }

        public void AddOccupant(int quantity)
        {
            OccupiedSpace += quantity;
            
            EventBus<OnHousingUpdated>.Publish(new OnHousingUpdated(MaxSpace, OccupiedSpace));
        } 
        
        public void RemoveOccupant(int quantity)
        {
            OccupiedSpace -= quantity;
            
            EventBus<OnHousingUpdated>.Publish(new OnHousingUpdated(MaxSpace, OccupiedSpace));
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

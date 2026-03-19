using System;
using _Scripts.AI.Entities.Pawn;
using _Scripts.BuildSystem.Building.Housing;
using _Scripts.Core;
using _Scripts.Events;
using UnityEngine;

namespace _Scripts.ImmigrationSystem
{
    public class ImmigrationManager : IDisposable
    {
        private readonly Vector2 _immigrationPos;
        private readonly HousingRegistry _housingRegistry;
        private readonly PawnSpawner _pawnSpawner;

        private int _maxInhabitants = 5;
        
        public ImmigrationManager(Vector2 immigrationPos ,HousingRegistry housingRegistry, PawnSpawner pawnSpawner)
        {
            _immigrationPos = immigrationPos;

            _housingRegistry = housingRegistry;
            _pawnSpawner = pawnSpawner;
            
            EventBus<OnNewDay>.Subscribe(StartImmigration);
        }

        private void StartImmigration(OnNewDay _)
        {
            if (_housingRegistry.OccupiedSpace >= _maxInhabitants)
                return;
            
            var inmigrants = GetImmigrationNumber();

            for (int i = 0; i < inmigrants; i++)
                _pawnSpawner.Spawn(_immigrationPos);
        } 

        private int GetImmigrationNumber()
        {
            var delta = _housingRegistry.MaxSpace - _housingRegistry.OccupiedSpace;

            return delta switch
            {
                > 10 => 2,
                > 0 => 1,
                _ => 0
            };
        }

        public void SetMaxInhabitants(int maxInhabitants)
        {
            _maxInhabitants = maxInhabitants;
        }

        public void Dispose()
        {
            EventBus<OnNewDay>.Unsubscribe(StartImmigration);
        }
    }
}

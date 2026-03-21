using System;
using _Scripts.AI.Entities.Pawn;
using _Scripts.BuildSystem.Building.Housing;
using _Scripts.Core;
using _Scripts.Events;
using _Scripts.NotificationSystem;
using UnityEngine;

namespace _Scripts.ImmigrationSystem
{
    public class ImmigrationManager : IDisposable
    {
        private readonly Vector2 _immigrationPos;
        private HousingRegistry _housingRegistry;
        private PawnSpawner _pawnSpawner;
        private NotificationManager _notificationManager;

        private int _maxInhabitants = 5;
        
        public ImmigrationManager(Vector2 immigrationPos) => _immigrationPos = immigrationPos;
        
        public void Init(ObjectResolver objectResolver)
        {
            _housingRegistry = objectResolver.Resolve<HousingRegistry>();
            _pawnSpawner = objectResolver.Resolve<PawnSpawner>();
            _notificationManager = objectResolver.Resolve<NotificationManager>();
            
            EventBus<OnNewDay>.Subscribe(StartImmigration);
        }

        private void StartImmigration(OnNewDay _)
        {
            if (_housingRegistry.OccupiedSpace >= _maxInhabitants)
                return;
            
            var immigrants = GetImmigrationNumber();

            int i;
            
            for (i = 0; i < immigrants; i++)
                _pawnSpawner.Spawn(_immigrationPos);
            
            if (i == 0) return;
            
            _notificationManager.Notify($"{i} immigrants have arrived to your clan", 5f);
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

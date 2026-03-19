using System;
using System.Collections.Generic;
using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Roles;
using _Scripts.Core;
using _Scripts.Core.UpdateManagement;
using _Scripts.DisasterSystem.Disasters;
using _Scripts.ResourcesSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.DisasterSystem
{
    public class DisasterManager : IUpdateObserver, IDisposable
    {
        private List<(IDisaster disaster, float weight)> _disasters;
        
        private IDisaster _activeDisaster;
        private float _timeRemaining;

        public DisasterManager()
        {
            UpdateManager.RegisterObserver(this);
        }

        public void Init(ObjectResolver objectResolver)
        {
            _disasters = new()
            {
                (new BlizzardDisaster(objectResolver.Resolve<RoleProductionRegistry>()), 30f),
                (new DiseaseDisaster(objectResolver.Resolve<PawnRegistry>(),
                    objectResolver.Resolve<PawnSpawner>()), 10f),
                (new FoodRotDisaster(objectResolver.Resolve<GameResourcesManager>()), 20f)
            };
        }

        public void TriggerRandom()
        {
            if (_activeDisaster != null) return;
            
            var disaster = PickWeighted();
            disaster.OnStart();
            
            if (disaster.Duration > 0f)
            {
                _activeDisaster = disaster;
                _timeRemaining = disaster.Duration;
            }
                
        }

        private IDisaster PickWeighted()
        {
            var total = 0f;
            foreach (var (_, weight) in _disasters) total += weight;

            var roll = Random.Range(0f, total);
            var cumulative = 0f;

            foreach (var (disaster, weight) in _disasters)
            {
                cumulative += weight;
                if (roll < cumulative) return disaster;
            }

            return _disasters[^1].disaster;
        }

        public void ObservedUpdate()
        {
            if (_activeDisaster == null) return;

            _timeRemaining -= Time.deltaTime;

            if (_timeRemaining <= 0f)
            {
                _activeDisaster.OnEnd();
                _activeDisaster = null;
            }
        }

        public void Dispose()
        {
            UpdateManager.UnregisterObserver(this);
        }
    }
}
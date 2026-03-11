using System;
using _Scripts.AI.Entities.Pawn.Roles;
using _Scripts.BuildSystem.Building;
using _Scripts.BuildSystem.Building.Housing;
using _Scripts.BuildSystem.Building.WorkPlace;
using _Scripts.Grid;
using _Scripts.ResourcesSystem;
using _Scripts.ResourcesSystem.Resources.ResourceTypes;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnModel
    {
        public PawnRoleType CurrentRole { get; set; } = PawnRoleType.None;
        public Cell CurrentCell;
        public WorkPlaceController WorkPlaceController {get; set;}
        public HouseController HouseController {get; set;}
        
        public Action<float> OnHealthChanged;
        public Action<float> OnHungerChanged;
        public Action OnDie;

        public bool IsHandlingUrgentState = false;
        
        public float HungerDecayRate {get; private set;} = 0.75f;
        
        public float HealthDecayRate {get; private set;} = 0.5f;
        
        public float MaxHealth { get; } = 100f;
        private float _health = 100;
        public float Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0, Health);
                OnHealthChanged?.Invoke(_health);
                
                if (_health > 0) return;
                OnDie?.Invoke();
            }
        }
        
        public float MaxHunger { get; } = 100f;
        private float _hunger = 100;
        public float Hunger
        {
            get => _hunger;
            set
            {
                _hunger = Mathf.Clamp(value, 0, MaxHunger);
                OnHungerChanged?.Invoke(_hunger);
            }
        }

        public float HungerThreshold { get; } = 30f;
        
        public ResourceTypeSO WhatIsFood { get; }

        public float Speed { get; set; } = 3f;

        public PawnModel(GridManager gridManager, Vector3 position, ResourceTypeSO food)
        {
            CurrentCell = gridManager.GetCell(
                GridUtils.WorldToGridPosition(position, gridManager.CellSize));
            WhatIsFood = food;
        }
    }
}

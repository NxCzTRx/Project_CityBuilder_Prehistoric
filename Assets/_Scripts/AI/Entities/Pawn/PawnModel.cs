using System;
using _Scripts.AI.Entities.Pawn.Roles;
using _Scripts.BuildSystem.Building;
using _Scripts.BuildSystem.Building.Housing;
using _Scripts.BuildSystem.Building.WorkPlace;
using _Scripts.Grid;
using _Scripts.ResourcesSystem;
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
        
        public float HungerDecayRate {get; private set;} = 0.75f;
        public float HealthDecayRate {get; private set;} = 0.5f;
        
        private float _health = 100;
        public float Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0, 100);
                OnHealthChanged?.Invoke(_health);
                
                if (_health > 0) return;
                OnDie?.Invoke();
            }
        }
        
        private float _hunger = 100;
        public float Hunger
        {
            get => _hunger;
            set
            {
                _hunger = Mathf.Clamp(value, 0, 100);
                OnHungerChanged?.Invoke(_hunger);
            }
        }

        public float Speed { get; set; } = 3f;

        public PawnModel(GridManager gridManager, Vector3 position)
        {
            CurrentCell = gridManager.GetCell(
                GridUtils.WorldToGridPosition(position, gridManager.CellSize));
        }
    }
}

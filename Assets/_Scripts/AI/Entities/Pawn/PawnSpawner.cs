using _Scripts.AI.Entities.Pawn.Scheduling;
using _Scripts.AI.FSM.States;
using _Scripts.BuildSystem.Building.Housing;
using _Scripts.Core;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnSpawner : MonoBehaviour
    {
        [SerializeField] private PawnEntity pawnEntityPrefab;
        private PawnRegistry _pawnRegistry;
        private HousingRegistry _housingRegistry;
        private PawnScheduler _pawnScheduler;
        
        private ObjectResolver _objectResolver;

        public void Init(ObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;

            _pawnRegistry = _objectResolver.Resolve<PawnRegistry>();
            _housingRegistry = objectResolver.Resolve<HousingRegistry>();
            _pawnScheduler = _objectResolver.Resolve<PawnScheduler>();
        }

        public void Spawn(Vector3 position)
        {
            if (!_housingRegistry.HasAvailableHousing)
            {
                Debug.LogError("No house available while spawning pawn");
            }
            
            var entity = Instantiate(pawnEntityPrefab, position, Quaternion.identity);
            entity.Init(_objectResolver, this);
            _pawnRegistry?.RegisterPawn(entity.PawnController);
            _housingRegistry?.GetAvailableHouse().AssignResident(entity.PawnController);
            _pawnScheduler.EvaluatePawn(entity.PawnController);
        }

        public void Despawn(PawnEntity entity)
        {
            _pawnRegistry.UnregisterPawn(entity.PawnController);
            entity.PawnController.Model.HouseController.RemoveResident(entity.PawnController);
            Destroy(entity.gameObject);
        }
    }
}

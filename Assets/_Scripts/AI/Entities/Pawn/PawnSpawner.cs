using _Scripts.AI.FSM.States;
using _Scripts.Core;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnSpawner : MonoBehaviour
    {
        [SerializeField] private PawnEntity pawnEntityPrefab;
        private PawnRegistry _registry;
        
        private ObjectResolver _objectResolver;

        public void Init(ObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;

            _registry = _objectResolver.Resolve<PawnRegistry>();
        }

        public void Spawn(Vector3 position)
        {
            var entity = Instantiate(pawnEntityPrefab, position, Quaternion.identity);
            entity.Init(_objectResolver, this);
            _registry?.RegisterPawn(entity.PawnController);
        }

        public void Despawn(PawnEntity entity)
        {
            _registry.UnregisterPawn(entity.PawnController);
            Destroy(entity.gameObject);
        }
    }
}

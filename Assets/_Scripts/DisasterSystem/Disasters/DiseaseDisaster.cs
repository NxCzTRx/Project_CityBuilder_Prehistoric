using _Scripts.AI.Entities.Pawn;
using UnityEngine;

namespace _Scripts.DisasterSystem.Disasters
{
    public class DiseaseDisaster : IDisaster
    {
        private readonly PawnRegistry _pawnRegistry;
        private readonly PawnSpawner _pawnSpawner;

        public DiseaseDisaster(PawnRegistry pawnRegistry, PawnSpawner pawnSpawner)
        {
            _pawnRegistry = pawnRegistry;
            _pawnSpawner = pawnSpawner;
        }
        
        public void OnStart()
        {
            var pawns = _pawnRegistry.GetAllPawns();
            if (pawns.Count == 0) return;

            var pawnToDie = pawns[Random.Range(0, pawns.Count)];
            pawnToDie.Die();
            Debug.Log("DISEASE DISASTER");
        }

        public void OnEnd()
        {

        }

        public float Duration { get; } = 0f;
    }
}

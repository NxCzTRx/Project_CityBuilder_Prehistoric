using _Scripts.AI.Entities.Pawn;
using UnityEngine;

namespace _Scripts.DisasterSystem.Disasters
{
    public class BlizzardDisaster : IDisaster
    {
        private readonly PawnRegistry _pawnRegistry;

        private const float ProductionMultiplierDuringDisaster = 0.75f;
        
        public BlizzardDisaster(PawnRegistry pawnRegistry)
        {
            _pawnRegistry = pawnRegistry;
        }
        
        public void OnStart()
        {
            foreach (var pawn in _pawnRegistry.GetAllPawns())
                pawn.ProductionMultiplier = ProductionMultiplierDuringDisaster;
            Debug.Log("Blizzard DISASTER started");
        }

        public void OnEnd()
        {
            foreach (var pawn in _pawnRegistry.GetAllPawns())
                pawn.ProductionMultiplier = 1f;
            Debug.Log("Blizzard DISASTER ended");
        }

        public float Duration { get; } = 100f;
    }
}

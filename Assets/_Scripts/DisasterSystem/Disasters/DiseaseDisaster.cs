using _Scripts.AI.Entities.Pawn;
using _Scripts.NotificationSystem;
using UnityEngine;

namespace _Scripts.DisasterSystem.Disasters
{
    public class DiseaseDisaster : IDisaster
    {
        private readonly PawnRegistry _pawnRegistry;
        private readonly PawnSpawner _pawnSpawner;
        private readonly NotificationManager _notificationManager;

        public DiseaseDisaster(PawnRegistry pawnRegistry, PawnSpawner pawnSpawner, NotificationManager notificationManager)
        {
            _pawnRegistry = pawnRegistry;
            _pawnSpawner = pawnSpawner;
            _notificationManager = notificationManager;
        }

        public void OnStart()
        {
            var pawns = _pawnRegistry.GetAllPawns();
            if (pawns.Count == 0) return;

            var pawnToDie = pawns[Random.Range(0, pawns.Count)];
            pawnToDie.Die();
            _notificationManager.Notify("A member has died due a severe disease.", 10f);
        }

        public void OnEnd()
        {

        }

        public float Duration { get; } = 0f;
    }
}

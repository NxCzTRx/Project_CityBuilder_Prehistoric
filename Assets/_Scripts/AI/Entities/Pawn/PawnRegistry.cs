using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.AI.Entities.Pawn.Roles;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnRegistry
    {
        private readonly List<PawnController> _pawns = new();

        public void RegisterPawn(PawnController pawnController)
        {
            _pawns.Add(pawnController);
            Debug.Log(_pawns.Count + " pawns registered");
        }

        public void UnregisterPawn(PawnController pawnController)
        {
            _pawns.Remove(pawnController);
        }

        public bool HasAvailablePawn => _pawns.Any(p => p.Model.CurrentRole == PawnRoleType.None);
    
        public PawnController GetAvailablePawn() =>
            _pawns.FirstOrDefault(p => p.Model.CurrentRole == PawnRoleType.None);

        public IReadOnlyList<PawnController> GetAllPawns() => _pawns;
    
        public IEnumerable<PawnController> GetPawnsByCondition(Func<PawnController, bool> predicate) => 
            _pawns.Where(predicate);
    }
}

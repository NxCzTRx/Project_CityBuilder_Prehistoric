using System.Collections.Generic;
using System.Linq;
using _Scripts.AI.Entities.Pawn;
using _Scripts.AI.Entities.Pawn.Roles;

public class PawnRegistry
{
    private readonly List<PawnController> _pawns = new();

    public void RegisterPawn(PawnController pawnController)
    {
        _pawns.Add(pawnController);
    }

    public void UnregisterPawn(PawnController pawnController)
    {
        _pawns.Remove(pawnController);
        //Destruir
    }

    public bool HasAvailablePawn => _pawns.Any(p => p.Model.CurrentRole == PawnRoleType.None);
    
    public PawnController GetAvailablePawn() =>
        _pawns.FirstOrDefault(p => p.Model.CurrentRole == PawnRoleType.None);
}

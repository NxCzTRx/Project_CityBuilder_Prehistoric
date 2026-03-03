using _Scripts.AI.Entities.Pawn.Roles;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.Entities.Pawn
{
    public class PawnModel
    {
        public GridManager GridManager { get; private set; }
        public PawnRoleType CurrentRole { get; set; } = PawnRoleType.None;
        public Cell CurrentCell;

        public float Speed { get; set; } = 3f;

        public PawnModel(GridManager gridManager, Vector3 position)
        {
            GridManager = gridManager;
            CurrentCell = GridManager.GetCell(GridUtils.WorldToGridPosition(position, gridManager.CellSize));
        }
        //public TaskManager TaskManager { get; }
    }
}

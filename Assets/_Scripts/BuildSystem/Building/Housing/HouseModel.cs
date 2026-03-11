using System.Collections.Generic;
using _Scripts.AI.Entities.Pawn;
using _Scripts.Grid;

namespace _Scripts.BuildSystem.Building.Housing
{
    public class HouseModel
    {
        public HouseSO HouseSO { get; }
        public Stack<PawnController> PawnResidents { get; } 
    
        public Cell EntranceCell;


        public HouseModel(HouseSO houseSO, Cell entranceCell)
        {
            HouseSO = houseSO;
            PawnResidents = new Stack<PawnController>(HouseSO.MaxResidents);
            
            EntranceCell = entranceCell;
        }
    }
}

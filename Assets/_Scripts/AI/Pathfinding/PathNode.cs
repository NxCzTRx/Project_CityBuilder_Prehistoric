using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.Pathfinding
{
    public class PathNode
    {
        public Cell Cell { get; }
        public PathNode Parent { get; set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost => GCost + HCost;

        public PathNode(Cell cell, PathNode parent, int gCost, int hCost)
        {
            Cell = cell;
            Parent = parent;
            GCost = gCost;
            HCost = hCost;
        }
    }
}

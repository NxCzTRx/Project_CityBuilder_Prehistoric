using System.Collections.Generic;
using System.Linq;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.AI.Pathfinding
{
    public static class AStar
    {
        public static IEnumerable<Vector2Int> FindPath(Cell initialCell, Cell goalCell, GridManager gridManager)
        {
            var openList = new List<PathNode>();
            var closedList = new HashSet<PathNode>();
            
            PathNode startNode = new PathNode(initialCell, null, 0, Heuristic(
                initialCell.Position,
                goalCell.Position));
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                var currentNode = openList[0];
                foreach (var node in openList)
                {
                    if (node.FCost < currentNode.FCost ||
                        (node.FCost == currentNode.FCost && node.HCost < currentNode.HCost))
                    {
                        currentNode = node;
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);
                
                if (currentNode.Cell == goalCell) 
                    return ReconstructPath(currentNode);

                foreach (var neighbour in GetNeighbours(currentNode, gridManager))
                {
                    if (closedList.Any(n => n.Cell == neighbour.Cell)) continue;

                    int tentativeGCost = currentNode.GCost + 1;

                    var existingNode = openList.FirstOrDefault(n => n.Cell == neighbour.Cell);

                    if (existingNode == null)
                    {
                        neighbour.Parent = currentNode;
                        neighbour.GCost = tentativeGCost;
                        neighbour.HCost = Heuristic(neighbour.Cell.Position, goalCell.Position);
                        openList.Add(neighbour);
                    }
                    else if (tentativeGCost < existingNode.GCost)
                    {
                        existingNode.GCost = tentativeGCost;
                        existingNode.Parent = currentNode;
                    }
                }
            }

            return null;
        }

        private static IEnumerable<Vector2Int> ReconstructPath(PathNode endNode)
        {
            var path = new List<Vector2Int>();
            var currentNode = endNode;

            while (currentNode != null)
            {
                path.Add(currentNode.Cell.Position);
                currentNode = currentNode.Parent;
            }

            path.Reverse();
            return path;
        }

        private static int Heuristic(Vector2Int start, Vector2Int goal)
        {
            return Mathf.Abs(goal.x - start.x) + Mathf.Abs(goal.y - start.y);
        }

        private static IEnumerable<PathNode> GetNeighbours(PathNode node, GridManager gridManager)
        {
            var neighbours = new List<PathNode>();
            var directions = new[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (var direction in directions)
            {
                var neighbourCell = gridManager.GetCell(node.Cell.Position + direction);
                if (neighbourCell != null && neighbourCell.IsWalkable)
                {
                    neighbours.Add(new PathNode(neighbourCell, node, 0, 0));
                }
            }

            return neighbours;
        }
    }
}
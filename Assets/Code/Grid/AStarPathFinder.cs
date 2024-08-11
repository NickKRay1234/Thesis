using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Grid
{
    public class AStarPathFinder : IPathFinder
    {
        private readonly GridData gridData;

        public AStarPathFinder(GridData gridData)
        {
            this.gridData = gridData;
        }

        public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
        {
            List<Vector2Int> openList = new List<Vector2Int>();
            HashSet<Vector2Int> closedList = new HashSet<Vector2Int>();
            Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
            Dictionary<Vector2Int, float> gScore = new Dictionary<Vector2Int, float>();
            Dictionary<Vector2Int, float> fScore = new Dictionary<Vector2Int, float>();

            openList.Add(start);
            gScore[start] = 0;
            fScore[start] = HeuristicCostEstimate(start, end);

            while (openList.Count > 0)
            {
                Vector2Int current = openList.OrderBy(node => fScore.ContainsKey(node) ? fScore[node] : float.MaxValue).First();

                if (current == end)
                {
                    return ReconstructPath(cameFrom, current);
                }

                openList.Remove(current);
                closedList.Add(current);

                foreach (Vector2Int neighbor in GetNeighbors(current))
                {
                    if (closedList.Contains(neighbor)) continue;
                    float tentativeGScore = gScore[current] + HeuristicCostEstimate(current, neighbor);
                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                    else if (tentativeGScore >= gScore[neighbor]) continue;

                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, end);
                }
            }

            return new List<Vector2Int>();
        }

        private List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
        {
            List<Vector2Int> totalPath = new List<Vector2Int> { current };
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Add(current);
            }
            totalPath.Reverse();
            return totalPath;
        }

        private List<Vector2Int> GetNeighbors(Vector2Int node)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            if (node.x - 1 >= 0) neighbors.Add(new Vector2Int(node.x - 1, node.y));
            if (node.x + 1 < gridData.gridWidth) neighbors.Add(new Vector2Int(node.x + 1, node.y));
            if (node.y - 1 >= 0) neighbors.Add(new Vector2Int(node.x, node.y - 1));
            if (node.y + 1 < gridData.gridHeight) neighbors.Add(new Vector2Int(node.x, node.y + 1));

            return neighbors;
        }

        private float HeuristicCostEstimate(Vector2Int a, Vector2Int b) =>
            Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y); // Расстояние Манхэттена
    }
}
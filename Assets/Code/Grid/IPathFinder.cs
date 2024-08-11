using System.Collections.Generic;
using UnityEngine;

namespace Code.Grid
{
    public interface IPathFinder
    {
        List<Vector2Int> FindPath(Vector2Int start, Vector2Int end);
    }
}
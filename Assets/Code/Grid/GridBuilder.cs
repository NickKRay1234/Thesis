using System.Linq;
using UnityEngine;

namespace Code.Grid
{
    [ExecuteInEditMode]
    public sealed class GridBuilder : MonoBehaviour
    {
        [SerializeField] 
        private GameObject gridPrefab;

        [SerializeField]
        private GridData gridData;

        private const float CellSize = 3.6f;
        private IPathFinder _pathFinder;

        private void OnEnable() =>
            gridData ??= new GridData();

        private void Start()
        {
            _pathFinder = new AStarPathFinder(gridData);
            if (!Application.isPlaying) 
                GenerateGrid();
        }

        public void ClearGrid() =>
            GridUtility.ClearGrid(gridData);

        public void GenerateGrid()
        {
            ClearGrid();
            gridData.Initialize();
            GridUtility.GenerateGrid(gridData, gridPrefab, CellSize, transform);
            GridUtility.PlaceStartAndEndPoints(gridData, gridPrefab, CellSize, transform);
            HighlightIntermediatePathPoints(gridData, _pathFinder);
        }

        private void HighlightIntermediatePathPoints(GridData fullGridData, IPathFinder pathFinder)
        {
            foreach (Vector2Int point in pathFinder.FindPath(fullGridData.StartPoint, fullGridData.EndPoint).Where(IsNotStartOrEndPoint))
                GridUtility.ChangeObjectColorAtPoint(fullGridData, point, Color.blue);
        }

        private bool IsNotStartOrEndPoint(Vector2Int point) => 
            !point.Equals(gridData.StartPoint) && point != gridData.EndPoint;
    }
}

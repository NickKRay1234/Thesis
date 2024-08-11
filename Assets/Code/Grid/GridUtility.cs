using UnityEngine;

namespace Code.Grid
{
    public static class GridUtility
    {
        public static void ClearGrid(GridData gridData)
        {
            if (gridData.SpawnedObjects == null) return;

            for (int x = 0; x < gridData.gridWidth; x++)
            for (int z = 0; z < gridData.gridHeight; z++)
                if (gridData.SpawnedObjects[x, z] is not null)
                    Object.DestroyImmediate(gridData.SpawnedObjects[x, z]);
            
            if (gridData.StartPoint != Vector2Int.zero)
                ClearPoint(gridData, ref gridData.StartPoint);
            
            if (gridData.EndPoint != Vector2Int.zero)
                ClearPoint(gridData, ref gridData.EndPoint);

            gridData.SpawnedObjects = null;
        }

        private static void ClearPoint(GridData gridData, ref Vector2Int point)
        {
            if (gridData.SpawnedObjects[point.x, point.y] is null) return;
            Object.DestroyImmediate(gridData.SpawnedObjects[point.x, point.y]);
            point = Vector2Int.zero;
        }

        public static void GenerateGrid(GridData gridData, GameObject prefab, float cellSize, Transform parent)
        {
            for (int x = 0; x < gridData.gridWidth; x++)
            for (int z = 0; z < gridData.gridHeight; z++)
                gridData.SpawnedObjects[x, z] = InstantiateGridObject(prefab, CalculateCellPosition(x, z, cellSize), parent);
        }

        public static void PlaceStartAndEndPoints(GridData gridData, GameObject prefab, float cellSize, Transform parent)
        {
            gridData.StartPoint = new Vector2Int(GetStartOrEndPointX(gridData), 0);
            gridData.EndPoint = new Vector2Int(GetStartOrEndPointX(gridData), gridData.gridHeight - 1);
            
            InstantiatePointObject(gridData.StartPoint, gridData, prefab, cellSize, parent);
            InstantiatePointObject(gridData.EndPoint, gridData, prefab, cellSize, parent);
        }

        public static void ChangeObjectColorAtPoint(GridData gridData, Vector2Int point, Color color)
        {
            Renderer component = gridData.SpawnedObjects[point.x, point.y].GetComponent<Renderer>();
            component.material.color = color;
        }

        private static int GetStartOrEndPointX(GridData gridData) => 
            gridData.gridWidth / 2;

        private static void InstantiatePointObject(Vector2Int point, GridData gridData, GameObject prefab, float cellSize, Transform parent)
        {
            var obj = InstantiateGridObject(prefab, CalculateCellPosition(point.x, point.y, cellSize), parent);
            gridData.SpawnedObjects[point.x, point.y] = obj;
        }

        private static Vector3 CalculateCellPosition(int x, int z, float cellSize) => new(x * cellSize, 0, z * cellSize);

        private static GameObject InstantiateGridObject(GameObject prefab, Vector3 position, Transform parent) =>
            Object.Instantiate(prefab, position, Quaternion.identity, parent);
    }
}
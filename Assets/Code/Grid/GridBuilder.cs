using System.Linq;
using UnityEngine;

namespace Code.Grid
{
    public sealed class GridBuilder : MonoBehaviour
    {
        [SerializeField] 
        private GameObject gridPrefab;
        
        [SerializeField]
        private GridData gridData;

        private const float CellSize = 3.6f;

        private void OnEnable() =>
            gridData ??= new GridData();
        
        private void Start() => 
            GenerateGrid();

        public void ClearGrid()
        {
            foreach (var obj in gridData.spawnedObjects.Where(obj => obj is not null))
                DestroyImmediate(obj);
            gridData.spawnedObjects.Clear();
        }

        public void GenerateGrid()
        {
            ClearGrid();

            for (int x = 0; x < gridData.gridWidth; x++)
                for (int z = 0; z < gridData.gridHeight; z++)
                    gridData.spawnedObjects.Add(InstantiateGridObject(CalculateCellPosition(x, z)));
        }

        private Vector3 CalculateCellPosition(int x, int z) =>
            new(x * CellSize, 0, z * CellSize);

        private GameObject InstantiateGridObject(Vector3 position) =>
            Instantiate(gridPrefab, position, Quaternion.identity, transform);
    }
}
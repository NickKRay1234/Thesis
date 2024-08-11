using System;
using UnityEngine;

namespace Code.Grid
{
    [Serializable]
    public sealed class GridData
    {
        [Range(0, 10)]
        public int gridWidth = 10;
        
        [Range(0, 10)]
        public int gridHeight = 10;
        
        [NonSerialized]
        public Vector2Int StartPoint;

        [NonSerialized]
        public Vector2Int EndPoint;
        
        [NonSerialized]
        public GameObject[,] SpawnedObjects;
        
        public void Initialize() =>
            SpawnedObjects = new GameObject[gridWidth, gridHeight];
    }
}
using System;
using System.Collections.Generic;
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
        
        public List<GameObject> spawnedObjects = new();
    }
}
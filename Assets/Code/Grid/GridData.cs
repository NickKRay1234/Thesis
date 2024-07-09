using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Grid
{
    [Serializable]
    public sealed class GridData
    {
        public int gridWidth = 10;
        public int gridHeight = 10;
        public List<GameObject> spawnedObjects = new();
    }
}
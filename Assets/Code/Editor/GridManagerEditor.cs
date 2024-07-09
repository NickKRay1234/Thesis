using UnityEditor;
using UnityEngine;

namespace Code
{
    [CustomEditor(typeof(GridManager))]
    public class GridManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GridManager gridManager = (GridManager)target;

            if (GUILayout.Button("Generate Grid"))
                gridManager.GenerateGrid();

            if (GUILayout.Button("Clear Grid"))
                gridManager.ClearGrid();
        }
    }
}
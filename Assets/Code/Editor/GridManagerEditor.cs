using Code.Grid;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    [CustomEditor(typeof(GridBuilder))]
    public class GridBuilderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GridBuilder gridManager = (GridBuilder)target;

            if (GUILayout.Button("Generate Grid"))
                gridManager.GenerateGrid();

            if (GUILayout.Button("Clear Grid"))
                gridManager.ClearGrid();
        }
    }
}
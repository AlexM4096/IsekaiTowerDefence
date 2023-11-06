using Dungeon.Visualization;
using UnityEditor;
using UnityEngine;

namespace Editor.DungeonGeneration
{
    [CustomEditor(typeof(DungeonVisualizer)), CanEditMultipleObjects]
    public class DungeonVisualizerEditor : UnityEditor.Editor
    {
        private DungeonVisualizer _dungeonVisualizer;

        private void Awake()
        {
            _dungeonVisualizer = (DungeonVisualizer)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Visualize Dungeon"))
                _dungeonVisualizer.VisualizeDungeon();
            
            if (GUILayout.Button("Clear Tilemap"))
                _dungeonVisualizer.ClearTilemap(); 
        }
    }
}
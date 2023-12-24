using DungeonSystem.Generation;
using UnityEditor;
using UnityEngine;

namespace Editors.DungeonGeneration
{
    [CustomEditor(typeof(DungeonController)), CanEditMultipleObjects]
    public class DungeonControllerEditor : Editor
    {
        private DungeonController _dungeonController;

        private void Awake()
        {
            _dungeonController = (DungeonController)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Dungeon"))
                _dungeonController.Generate();
            
            if (GUILayout.Button("Clear Dungeon"))
                _dungeonController.Clear();
            
            if (GUILayout.Button("Clear GameObjects"))
                _dungeonController.ClearGameObjects();
        }
    }
}
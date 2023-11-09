using DungeonSystem.Generation;
using UnityEditor;
using UnityEngine;

namespace Editors.DungeonGeneration
{
    [CustomEditor(typeof(DungeonGeneratorController)), CanEditMultipleObjects]
    public class DungeonGeneratorEditor : Editor
    {
        private DungeonGeneratorController _dungeonGeneratorController;

        private void Awake()
        {
            _dungeonGeneratorController = (DungeonGeneratorController)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Generate Dungeon"))
                _dungeonGeneratorController.Generate();
            
            if (GUILayout.Button("Clear GameObjects"))
                _dungeonGeneratorController.Clear();
        }
    }
}
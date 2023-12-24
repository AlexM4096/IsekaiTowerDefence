using UnityEditor;
using UnityEngine;

namespace Editors
{
    [CustomEditor(typeof(Test))]
    public class TestEditor : Editor
    {
        private Test _test;

        private void Awake()
        {
            _test = (Test)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Test Button"))
                _test.ToTest();
        }
    }
}
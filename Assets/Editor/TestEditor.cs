using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Test))]
    public class TestEditor : UnityEditor.Editor
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
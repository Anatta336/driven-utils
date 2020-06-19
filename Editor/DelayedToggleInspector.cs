using UnityEngine;
using UnityEditor;

namespace SamDriver.Util
{
    [CustomEditor(typeof(DelayedToggle))]
    public class DelayedToggleInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var item = (DelayedToggle)target;
            GUILayout.Label($"{(bool)item}");
            Repaint();
        }
    }
}

using UnityEngine;
using UnityEditor;

namespace SamDriver.Util
{
    [CustomEditor(typeof(FloatMB))]
    public class FloatMBInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var item = (FloatMB)target;
            item.Value = EditorGUILayout.FloatField(item.Value);
        }
    }
}

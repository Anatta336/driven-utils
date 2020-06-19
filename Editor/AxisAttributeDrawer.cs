using UnityEditor;
using UnityEngine;

namespace SamDriver.Util
{
    [CustomPropertyDrawer(typeof(AxisAttribute))]
    class AxisAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Axis selectedAxis = (Axis)EditorGUI.EnumPopup(position, label, (Axis)property.enumValueIndex);
            property.enumValueIndex = (int)selectedAxis;
        }
    }
}

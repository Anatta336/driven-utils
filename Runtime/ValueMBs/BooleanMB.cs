using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SamDriver.Util
{
    /// <summary>
    /// Holds a single boolean value and publishes an event when it changes.
    /// </summary>
    public class BooleanMB : MonoBehaviour
    {
        public string Name = "Boolean";
        public bool Value
        {
            get => _val;
            set
            {
                bool old = _val;
                _val = value;
                if (_val != old)
                {
                    RaiseValueChange(_val);
                }
            }
        }
        bool _val;

        /// <summary>
        /// Event is published whenever this Value property changes.
        /// Note that this could happen multiple times a frame. Beware doing
        /// heavy lifting in direct response.
        /// </summary>
        public event Action<bool> OnValueChange;
        void RaiseValueChange(bool value) => OnValueChange?.Invoke(value);
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(BooleanMB))]
    public class BooleanMBEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var item = (BooleanMB)target;
            var value = item.Value;
            GUILayout.Label($"Value: {value}");
        }

        override public bool RequiresConstantRepaint() => true;
    }
    #endif
}

using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SamDriver.Util
{
    /// <summary>
    /// Holds a single float value and publishes an event when it changes.
    /// </summary>
    public class FloatMB : MonoBehaviour
    {
        public string Name = "Float";
        public float Value
        {
            get => _val;
            set
            {
                float old = _val;
                _val = value;
                if (!Mathf.Approximately(old, _val))
                {
                    RaiseValueChange(_val);
                }
            }
        }
        float _val;

        /// <summary>
        /// Event is published whenever this FloatSO's Value property changes.
        /// Note that this could happen multiple times a frame. Beware doing
        /// heavy lifting in direct response.
        /// </summary>
        public event Action<float> OnValueChange;
        void RaiseValueChange(float value) => OnValueChange?.Invoke(value);
    }

    
    #if UNITY_EDITOR
    [CustomEditor(typeof(FloatMB))]
    public class FloatMBEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var item = (FloatMB)target;
            var value = item.Value;
            GUILayout.Label($"Value: {value.ToString("F5")}");
        }

        override public bool RequiresConstantRepaint() => true;
    }
    #endif

}

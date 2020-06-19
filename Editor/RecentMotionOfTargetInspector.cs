using UnityEngine;
using UnityEditor;

namespace SamDriver.Util
{
    [CustomEditor(typeof(RecentMotionOfTarget))]
    public class RecentMotionOfTargetInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var item = (RecentMotionOfTarget)target;

            if (Application.isPlaying)
            {
                // GUILayout.Label($"velocity: {item.AverageVelocity.ToString("F3")}");
                GUILayout.Label($"recent positions:\n{item.DumpRecentPositions()}");
                Repaint();
            }
        }
    }
}

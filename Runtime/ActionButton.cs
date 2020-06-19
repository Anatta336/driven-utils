// This convenient tool is heavily based on:
// https://github.com/jmickle66666666/JazzBox/blob/master/Scripts/ActionButton.cs

using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SamDriver.Util
{
    public class ActionButton : MonoBehaviour
    {
        public string ButtonName = "Run";
        public bool IsBeingEdited = true;
        public UnityEvent ActionToRun;

        public void Run()
        {
            ActionToRun.Invoke();
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(ActionButton))]
    public class ActionButtonInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var actionButton = (target as ActionButton);

            if (actionButton.IsBeingEdited)
            {
                DrawDefaultInspector();
                if (GUILayout.Button(actionButton.ButtonName))
                {
                    actionButton.Run();
                }
            }
            else
            {
                GUILayout.BeginHorizontal();
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(actionButton.ButtonName))
                    {
                        actionButton.Run();
                    }

                    if (GUILayout.Button("Edit"))
                    {
                        actionButton.IsBeingEdited = true;
                    }
                }
            }
        }
    }
    #endif
}

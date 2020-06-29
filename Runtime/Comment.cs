using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SamDriver.Util
{
    /*
    // Minimal version:
    public class Comment : MonoBehaviour
    {
        // TextArea(minLines, maxLines) - scroll bar appears when above maxLines
        [TextArea(1, 10)]
        public string Content = "";
    }
    */

    public class Comment : MonoBehaviour
    {
        // CS0414 "field assigned but never used", which is intentional
        // (it is used through serialization, but the compiler doesn't recognise that)
        #pragma warning disable CS0414
        [SerializeField] string content = "";
        #pragma warning restore CS0414
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Comment))]
    [CanEditMultipleObjects]
    public class CommentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var property = serializedObject.FindProperty("content");
            property.stringValue = GUILayout.TextArea(property.stringValue);
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}

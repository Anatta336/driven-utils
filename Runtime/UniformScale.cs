using UnityEngine;

namespace SamDriver.Util
{
    [ExecuteInEditMode]
    public class UniformScale : MonoBehaviour
    {
        public float Scale = 1f;
        Vector3 scaleVector;

        void OnEnable()
        {
            scaleVector = new Vector3(Scale, Scale, Scale);
        }

        void OnValidate() => ApplyScale();
        void Update() => ApplyScale();

        void ApplyScale()
        {
            scaleVector.Set(Scale, Scale, Scale);
            transform.localScale = scaleVector;
        }
    }
}
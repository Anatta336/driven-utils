using UnityEngine;

namespace SamDriver.Util
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    /// <summary>
    /// Sets the bounds of the mesh used by MeshFilter to be very large,
    /// effectively disabling Unity's basic mesh culling.
    /// </summary>
    public class LargeBounds : MonoBehaviour
    {
        void OnEnable()
        {
            var mesh = GetComponent<MeshFilter>().sharedMesh;
            mesh.bounds = new Bounds(Vector3.zero, new Vector3(1000f, 1000f, 1000f));
        }
    }
}

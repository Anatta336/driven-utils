using UnityEngine;

namespace SamDriver.Util
{
    [ExecuteInEditMode]
    public class JointPointAtAnchor : MonoBehaviour
    {
        [SerializeField] Joint _joint = default;

        void Update()
        {
            if (_joint == null) return;
            
            var connectedBody = _joint.connectedBody;
            if (connectedBody == null) return;

            var worldAnchorOnConnected = connectedBody.transform.TransformPoint(_joint.connectedAnchor);
            var hereToThere = worldAnchorOnConnected - transform.position;
            
            // rotation gets messy at small distances (and impossible at zero)
            if (hereToThere.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(hereToThere);
            }
        }
    }
}

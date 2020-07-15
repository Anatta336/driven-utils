using UnityEngine;

namespace SamDriver.Util
{
    [RequireComponent(typeof(Joint))]
    [ExecuteInEditMode]
    public class JointPointAtAnchor : MonoBehaviour
    {
        Joint joint
        {
            get
            {
                if (joint_ == null) joint_ = GetComponent<Joint>();
                return joint_;
            }
        }
        Joint joint_;

        void Update()
        {
            var connectedBody = joint.connectedBody;
            if (connectedBody == null) return;

            var worldAnchorOnConnected = connectedBody.transform.TransformPoint(joint.connectedAnchor);
            var hereToThere = worldAnchorOnConnected - transform.position;
            
            // rotation gets messy at small distances (and impossible at zero)
            if (hereToThere.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(hereToThere);
            }
        }
    }
}

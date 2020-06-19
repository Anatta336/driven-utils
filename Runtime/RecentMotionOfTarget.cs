using UnityEngine;

namespace SamDriver.Util
{
    public class RecentMotionOfTarget : RecentMotion
    {
        Rigidbody _target;
        public Rigidbody Target
        {
            get => _target;
            set
            {
                if (_target == value) return;
                _target = value;
                ResetRecord();
            }
        }

        override protected Vector3 GetPosition()
        {
            if (Target == null) return Vector3.zero;
            return Target.worldCenterOfMass;
        }
    }
}

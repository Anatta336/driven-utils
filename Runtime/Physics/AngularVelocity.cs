using UnityEngine;

namespace SamDriver.Util
{
    public static class AngularVelocity
    {
        public static Quaternion RotationFromAngularVelocityRadians(Vector3 angularVelocityRadians)
        {
            float theta = angularVelocityRadians.magnitude;
            Vector3 axisOfRotation = angularVelocityRadians / theta;

            float sinThetaOverTwo = Mathf.Sin(theta / 2f);
            Quaternion rotation = new Quaternion(
                axisOfRotation.x * sinThetaOverTwo,
                axisOfRotation.y * sinThetaOverTwo,
                axisOfRotation.z * sinThetaOverTwo,
                Mathf.Cos(theta / 2f)
            );
            return rotation;
        }

        public static Quaternion RotationFromAngularVelocityRadians(float xRadians, float yRadians, float zRadians)
        {
            Vector3 angularVelocityRadians = new Vector3(xRadians, yRadians, zRadians);
            return RotationFromAngularVelocityRadians(angularVelocityRadians);
        }

        public static Quaternion RotationFromAngularVelocityDegrees(Vector3 angularVelocityDegrees)
        {
            return RotationFromAngularVelocityRadians(angularVelocityDegrees * Mathf.Deg2Rad);
        }

        public static Quaternion RotationFromAngularVelocityDegrees(float xDegrees, float yDegrees, float zDegrees)
        {
            Vector3 angularVelocityDegrees = new Vector3(xDegrees, yDegrees, zDegrees);
            return RotationFromAngularVelocityDegrees(angularVelocityDegrees);
        }
    }
}

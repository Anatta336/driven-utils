using UnityEngine;

namespace SamDriver.Util
{
    public static class AngularVelocity
    {
        public static Quaternion RotationFromAngularVelocityRadians(Vector3 angularVelocityRadians)
        {
            float theta = angularVelocityRadians.magnitude;
            if (theta == 0f)
            {
                return Quaternion.identity;
            }

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

        public static Vector3 AngularVelocityRadiansFromQuaternion(Quaternion rotation)
        {
            float thetaOverTwo = Mathf.Acos(rotation.w);
            if (float.IsNaN(thetaOverTwo))
            {
                return Vector3.zero;
            }

            float sinThetaOverTwo = Mathf.Sin(thetaOverTwo);

            if (sinThetaOverTwo == 0f)
            {
                return Vector3.zero;
            }

            Vector3 axisOfRotation = new Vector3(
                rotation.x / sinThetaOverTwo,
                rotation.y / sinThetaOverTwo,
                rotation.z / sinThetaOverTwo
            );
            return axisOfRotation * (thetaOverTwo * 2f);
        }

        public static Vector3 AngularVelocityDegreesFromQuaternion(Quaternion rotation)
        {
            return AngularVelocityRadiansFromQuaternion(rotation) * Mathf.Rad2Deg;
        }
    }
}

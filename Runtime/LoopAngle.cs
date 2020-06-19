namespace SamDriver.Util
{
    public static class LoopAngle
    {
        /// <summary>
        /// Takes an angle in degrees and converts it to the range (-180°, 180°]
        /// That is, exclusive of -180° and inclusive of 180°.
        /// </summary>
        public static float LoopDegrees(float angleDegrees)
        {
            // first loop to [0°, 360°)
            if (angleDegrees >= 360f)
            {
                // range [360, inf]

                angleDegrees = angleDegrees % 360f;
                // range [0, 360)
            }
            else if (angleDegrees < 0f)
            {
                // range [-inf, 0)

                angleDegrees = angleDegrees % 360f;
                // range (-360, 0]

                angleDegrees += 360f;
                // range (0, 360]

                angleDegrees = angleDegrees % 360f;
                // range [0, 360)
            }

            // use negative values rather than turning more than 180°
            if (angleDegrees > 180f)
            {
                angleDegrees = (angleDegrees - 360f);
            }
            // range (-180, 180]

            return angleDegrees;
        }

        const float π = 3.1415926535897932384626433832795f;

        /// <summary>
        /// Takes an angle in degrees and converts it to the range (-π, π]
        /// That is, exclusive of -π° and inclusive of π.
        /// </summary>
        public static float LoopRadians(float angleRadians)
        {
            // first loop to [0, 2π)
            if (angleRadians >= 2f * π)
            {
                angleRadians = angleRadians % (2f * π);
            }
            else if (angleRadians < 0f)
            {
                // range [-inf, 0)

                angleRadians = angleRadians % (2f * π);
                // range (-2π, 0]

                angleRadians += (2f * π);
                // range (0, 2π]

                angleRadians = angleRadians % (2f * π);
                // range [0, 2π)
            }

            // use negative values rather than turning more than π
            if (angleRadians > π)
            {
                angleRadians = (angleRadians - (2f * π));
            }
            // range (-π, π]

            return angleRadians;
        }
    }
}

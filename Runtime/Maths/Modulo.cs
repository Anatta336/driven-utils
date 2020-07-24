using UnityEngine;

namespace SamDriver.Util
{
    public static class Modulo
    {
        /// <summary>
        /// Similar to (a % b) but performs modulo rather than remainder.
        /// They'd give same result for positive values, but differ in negative.
        /// This form is useful for, for example, looping around the index of an array.
        /// </summary>
        public static int Mod(int a, int b)
        {
            return ((a %= b) < 0) ? (a + b) : a;
        }
    }
}

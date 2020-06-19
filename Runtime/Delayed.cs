using UnityEngine;
using System;
using System.Collections;

namespace SamDriver.Util
{
    public class Delayed
    {
        /// <summary>
        /// You likely don't want to just directly call this function.
        /// Example use:
        ///    StartCoroutine(ActionAfterDelay(5f, () =>
        ///    {
        ///        Debug.Log("5 seconds have passed.");
        ///    }));
        /// </summary>
        public static IEnumerator ActionAfterDelay(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    }
}

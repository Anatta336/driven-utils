using System.Collections.Generic;

namespace SamDriver.Util
{
    public interface ITargetProvider<T> : ISuppressable, IEnumerable<T>
    {
        /// <summary>
        /// Up to one potential target, selected by whatever means are desired.
        /// May return null when the target provider decides there's no valid target.
        /// </summary>
        T Target { get; }

        /// <summary>
        /// True if there is currently a potential target.
        /// </summary>
        bool HasTarget { get; }
    }
}

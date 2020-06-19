using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SamDriver.Util
{
    /// <summary>
    /// A collection of items, which counts how many times each unique item is within the collection.
    /// Enumerating the collection gives each item in the collection only once, in arbitrary order.
    /// </summary>
    public class CountedSet<T> : IEnumerable<T>
    {
        Dictionary<T, uint> backingDict = new Dictionary<T, uint>();

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return backingDict.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return backingDict.Keys.GetEnumerator();
        }

        /// <summary>
        /// True if 1 or more instances of the item are in collection.
        /// </summary>
        public bool Contains(T item)
        {
            return backingDict.ContainsKey(item);
        }

        public bool IsEmpty { get => (backingDict.Count == 0); }
        public int KeyCount { get => backingDict.Count; }

        /// <summary>
        /// Count of how many instances of the item are in collection.
        /// </summary>
        public uint CountOf(T item)
        {
            if (!Contains(item)) return 0;
            
            return backingDict[item];
        }

        /// <summary>
        /// Adds one instance of given item to collection
        /// </summary>
        /// <returns>Count of how many instances of the item are now in collection.</returns>
        public uint Add(T item)
        {
            if (backingDict.ContainsKey(item))
            {
                return ++backingDict[item];
            }
            else
            {
                backingDict.Add(item, 1);
                return 1;
            }
        }

        /// <summary>
        /// Removes one instance of given item from collection.
        /// </summary>
        /// <returns>Count of how many instances of the item remain in collection.</returns>
        public uint Remove(T item)
        {
            if (!Contains(item))
            {
                Debug.LogWarning($"Attempt to remove {item} which isn't in the {nameof(CountedSet<T>)}.");
                return 0;
            }

            uint count = backingDict[item] - 1;
            if (count == 0)
            {
                backingDict.Remove(item);
            }
            else
            {
                backingDict[item] = count;
            }
            return count;
        }

        /// <summary>
        /// Removes all instances of given item from collection.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveWholly(T item)
        {
            if (!Contains(item))
            {
                Debug.LogWarning($"Attempt to wholly remove {item} which isn't in the {nameof(CountedSet<T>)}.");
                return;
            }

            backingDict.Remove(item);            
        }

        /// <summary>
        /// Removes all items and their counts from the collection.
        /// </summary>
        public void Clear()
        {
            backingDict.Clear();
        }
    }
}

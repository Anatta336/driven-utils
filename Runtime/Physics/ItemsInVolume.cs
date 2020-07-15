using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SamDriver.Util
{
    public class ItemsInVolume<T> : IEnumerable<T> where T : MonoBehaviour
    {
        CountedSet<T> contents = new CountedSet<T>();

        public bool IsEmpty { get => contents.IsEmpty; }
        public int Count { get => contents.KeyCount; }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)contents).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)contents).GetEnumerator();
        }

        /// <summary>
        /// Published when an item first enters the volume(s).
        /// Not published when an item that's already inside enters a second volume.
        /// </summary>
        public event Action<T> OnItemEnter;
        void RaiseItemEnter(T item) => OnItemEnter?.Invoke(item);

        /// <summary>
        /// Published when an item completely leaves the volume(s).
        /// </summary>
        public event Action<T> OnItemExit;
        void RaiseItemExit(T item) => OnItemExit?.Invoke(item);

        public void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody == null) return;

            T item = other.attachedRigidbody.GetComponentInChildren<T>();
            if (item == null) return;

            var newCount = contents.Add(item);
            if (newCount == 1) RaiseItemEnter(item);
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.attachedRigidbody == null) return;
            
            T item = other.attachedRigidbody.GetComponentInChildren<T>();
            if (item == null) return;

            if (!contents.Contains(item))
            {
                Debug.LogWarning($"Trying to remove {item} from {nameof(ItemsInVolume<T>)} when not present.");
            }
            else
            {
                var newCount = contents.Remove(item);
                if (newCount == 0) RaiseItemExit(item);
            }
        }

        public void OnItemDisabled(T item)
        {
            contents.RemoveWholly(item);
        }
    }
}

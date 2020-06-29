using UnityEngine;
using System.Collections.Generic;

namespace SamDriver.Util
{
    /// <summary>
    /// Maintains a collection of ISuppressables to apply suppression to.
    /// </summary>
    public class Suppressor : MonoBehaviour
    {
        bool _isSuppressing;
        public bool IsSupressing
        {
            get => _isSuppressing;
            set
            {
                if (value == _isSuppressing) return;
                _isSuppressing = value;
                if (_isSuppressing)
                {
                    BeginSuppression();
                }
                else
                {
                    EndSuppression();
                }
            }
        }

        [SerializeField] EventMB triggerBegin = default;
        [SerializeField] EventMB triggerEnd = default;

        // typed as MonoBehaviour to allow components to be easily set in the inspector,
        // will only actually add those that implement ISuppressable.
        [SerializeField] MonoBehaviour[] componentsToSuppress = default;

        HashSet<ISuppressable> Suppressables = new HashSet<ISuppressable>();

        void OnEnable()
        {
            foreach (var item in componentsToSuppress)
            {
                ISuppressable suppressable = item as ISuppressable;
                if (suppressable == null)
                {
                    Debug.LogWarning($"{item} placed on {nameof(componentsToSuppress)} but is not ${nameof(ISuppressable)}");
                    continue;
                }
                AddSuppressable(suppressable);
            }
            if (triggerBegin != null) triggerBegin.OnEvent += OnTriggerBegin;
            if (triggerEnd != null) triggerEnd.OnEvent += OnTriggerEnd;
        }

        void OnDisable()
        {
            if (triggerBegin != null) triggerBegin.OnEvent -= OnTriggerBegin;
            if (triggerEnd != null) triggerEnd.OnEvent -= OnTriggerEnd;
        }

        void OnTriggerBegin()
        {
            IsSupressing = true;
        }

        void OnTriggerEnd()
        {
            IsSupressing = false;
        }

        /// <summary>
        /// Adds a ISuppressable to the collection of things to suppress.
        /// If this suppression is active, applies that to the newly added item.
        /// </summary>
        /// <returns>False if the ISuppressable was already in the collection, and so wasn't added.</returns>
        public bool AddSuppressable(ISuppressable item)
        {
            bool wasAdded = Suppressables.Add(item);

            if (IsSupressing)
            {
                item.AddSuppress(this);
            }

            return wasAdded;
        }

        /// <summary>
        /// Removes an ISuppressable from the collection of things to suppress.
        /// If it was being suppressed from here, removes that suppression.
        /// </summary>
        /// <returns>True if the Isuppressable was in the collection and so was able to be removed.</returns>
        public bool RemoveSuppressable (ISuppressable item)
        {
            bool wasRemoved = Suppressables.Remove(item);

            if (IsSupressing)
            {
                item.RemoveSuppress(this);
            }

            return wasRemoved;
        }

        void BeginSuppression()
        {
            foreach (var suppressable in Suppressables)
            {
                suppressable.AddSuppress(this);
            }
        }

        void EndSuppression()
        {
            foreach (var suppressable in Suppressables)
            {
                suppressable.RemoveSuppress(this);
            }
        }
    }
}

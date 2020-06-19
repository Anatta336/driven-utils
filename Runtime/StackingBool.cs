using System.Collections.Generic;
using System;

namespace SamDriver.Util
{
    /// <summary>
    /// When multiple things might make something true and you want to know when any
    /// of them are present.
    /// </summary>
    public class StackingBool
    {
        public static explicit operator bool(StackingBool item)
        {
            return !object.ReferenceEquals(item, null) && item.Value;
        }

        /// <summary>
        /// Event raised when StackingBool transitions to true.
        /// </summary>
        public event Action OnBecomeTrue;
        void RaiseBecomeTrue() => OnBecomeTrue?.Invoke();

        /// <summary>
        /// Event published when StackingBool transitions to false.
        /// </summary>
        public event Action OnBecomeFalse;
        void RaiseBecomeFalse() => OnBecomeFalse?.Invoke();

        public bool Value { get; protected set; }
        public int SetCount { get => truthSet.Count; }
        
        HashSet<object> truthSet = new HashSet<object>();

        public void AddTrue(object obj)
        {
            truthSet.Add(obj);
            UpdateValue();
        }
        public void RemoveTrue(object obj)
        {
            truthSet.Remove(obj);
            UpdateValue();
        }

        protected void UpdateValue()
        {
            bool previous = Value;
            Value = (truthSet.Count > 0);
            if (!previous && Value)
            {
                RaiseBecomeTrue();
            }
            else if (previous && !Value)
            {
                RaiseBecomeFalse();
            }
        }

        public override string ToString()
        {
            // when truthSet is empty: "false(0)"
            // when truthSet has 4 items: "true(4)"
            return $"{(bool)this}({SetCount})";
        }
    }
}

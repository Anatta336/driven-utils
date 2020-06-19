using UnityEngine;
using System;

namespace SamDriver.Util
{
    /// <summary>
    /// Note this only supports delay for turning off, otherwise it'd still be
    /// vulnerable to messiness. If you do need to do delayed on and off, look
    /// at using something like StackingBool.
    /// </summary>
    public class DelayedToggle : MonoBehaviour
    {
        public static explicit operator bool(DelayedToggle item)
        {
            return !object.ReferenceEquals(item, null) && item.Value;
        }

        bool _value;
        public bool Value
        {
            get => _value;
            private set
            {
                if (_value == value) return;
                _value = value;
                OnChange();
            }
        }

        public bool IsWaitingForDelayedOff { get; private set; }
        
        /// <summary>
        /// Event raised when toggled on.
        /// </summary>
        public event Action OnToggleOn;
        void RaiseToggleOn() => OnToggleOn?.Invoke();

        /// <summary>
        /// Event raised when toggled off. Whether immediately or after a delay.
        /// </summary>
        public event Action OnToggleOff;
        void RaiseToggleOff() => OnToggleOff?.Invoke();

        void OnChange()
        {
            if (Value)
            {
                RaiseToggleOn();
            }
            else
            {
                RaiseToggleOff();
            }
        }

        /// <summary>
        /// Immediately set this toggle to on. Cancels any delayed turn offs.
        /// </summary>
        public void TurnOn()
        {
            StopAllCoroutines();
            IsWaitingForDelayedOff = false;
            Value = true;
        }

        /// <summary>
        /// Immediately set this toggle to off. Cancels any delayed turn offs.
        /// </summary>
        public void TurnOff()
        {
            StopAllCoroutines();
            IsWaitingForDelayedOff = false;
            Value = false;
        }

        /// <summary>
        /// Begin to wait a specified time, after which set this toggle to off.
        /// Any existing delayed turn off will be cancelled and replaced by this.
        /// </summary>
        /// <param name="delay">Time in seconds after which to turn off this toggle.</param>
        public void TurnOffAfterDelay(float delay)
        {
            StopAllCoroutines();

            if (delay <= 0f)
            {
                TurnOff();
                return;
            }

            IsWaitingForDelayedOff = true;
            StartCoroutine(Delayed.ActionAfterDelay(delay, () =>
            {
                Value = false;
                IsWaitingForDelayedOff = false;
            }));
        }

        /// <summary>
        /// Cancels any delayed turn off.
        /// </summary>
        /// <returns>true if there was a delayed turn off to cancel.</returns>
        public bool CancelDelayedTurnOff()
        {
            bool wasWaiting = IsWaitingForDelayedOff;
            StopAllCoroutines();
            IsWaitingForDelayedOff = false;

            return wasWaiting;
        }
    }
}

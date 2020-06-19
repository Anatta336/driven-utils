using UnityEngine;
using System;

namespace SamDriver.Util
{
    /// <summary>
    /// Linearly interpolates a FloatMB value following specified limits.
    /// </summary>
    [ExecuteInEditMode]
    public class LinearFader : MonoBehaviour
    {
        public FloatMB Current;

        public bool IsChanging
        {
            get { return updateFunction != null; }
        }

        /// <summary>
        /// Published the moment a fade is called for, before any change to value happens.
        /// </summary>
        public event Action OnFadeBegin;
        void RaiseFadeBegin() => OnFadeBegin?.Invoke();

        /// <summary>
        /// Published when target value has been reached.
        /// </summary>
        public event Action OnFadeComplete;
        void RaiseFadeComplete() => OnFadeComplete?.Invoke();

        float target;
        float changePerSecond;
        float fadeBeginTime;
        float duration;
        Action updateFunction = null;

        void OnEnable()
        {
            if (Current == null) Debug.LogError("Requires non-null Current FloatMB.");
        }

        void Update()
        {
            updateFunction?.Invoke();
        }

        public void FadeByRate(float target, float changePerSecond)
        {
            this.target = target;
            this.changePerSecond = changePerSecond;

            updateFunction = UpdateByRate;

            RaiseFadeBegin();
        }

        public void FadeByDuration(float target, float duration)
        {
            this.target = target;
            this.duration = duration;

            fadeBeginTime = Time.time;
            updateFunction = UpdateByDuration;

            RaiseFadeBegin();
        }

        public void FadeByRateWithTimeLimit(float target, float changePerSecond, float maximumDuration)
        {
            this.target = target;
            this.changePerSecond = changePerSecond;
            this.duration = maximumDuration;

            fadeBeginTime = Time.time;
            updateFunction = UpdateByRateWithTimeLimit;

            RaiseFadeBegin();
        }

        void UpdateByRate()
        {
            float difference = target - Current.Value;
            float maxChangeThisUpdate = changePerSecond * Time.deltaTime;
            difference = Mathf.Clamp(difference, -maxChangeThisUpdate, maxChangeThisUpdate);
            Current.Value += difference;

            CheckForCompletion();
        }

        void UpdateByDuration()
        {
            float changePerSecondToBeOnSchedule = ChangePerSecondNeeded();
            Current.Value += changePerSecondToBeOnSchedule * Time.deltaTime;

            CheckForCompletion();
        }

        void UpdateByRateWithTimeLimit()
        {
            float rateToBeatTimeLimit = ChangePerSecondNeeded();
            float rateToUse;
            if ((rateToBeatTimeLimit >= 0f && rateToBeatTimeLimit > changePerSecond) ||
                (rateToBeatTimeLimit < 0f && rateToBeatTimeLimit < changePerSecond))
            {
                rateToUse = rateToBeatTimeLimit;
            }
            else
            {
                rateToUse = changePerSecond;
            }
            Current.Value += rateToUse * Time.deltaTime;

            CheckForCompletion();
        }

        float ChangePerSecondNeeded()
        {
            float difference = target - Current.Value;
            float timeRemaining = (fadeBeginTime + duration) - Time.time;
            return difference / timeRemaining;
        }

        void CheckForCompletion()
        {
            if (Mathf.Approximately(Current.Value, target))
            {
                updateFunction = null;
                RaiseFadeComplete();
            }
        }
    }
}

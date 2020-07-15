using UnityEngine;

namespace SamDriver.Util
{
    /// <summary>
    /// Represents recent motion of the GameObject. A "physics step" is Unity's fixed update.
    /// On creation and reset the recent motion record is filled with current position.
    /// So for movingAverageStepCount frames after reset/creation moving average is biased.
    /// 
    /// TODO: Could use a curve to define weighted average rather than simple mean.
    /// </summary>
    public class RecentMotion : MonoBehaviour
    {
        // undefined behaviour if changed after OnEnable
        [SerializeField] int movingAverageStepCount = 6;

        Vector3[] recentPositions;

        /// <summary>
        /// The index that will be written to in the next FixedUpdate.
        /// Because the array loops, it's also the index of the oldest stored position.
        /// </summary>
        int currentIndex = 0;

        bool isAverageVelocityOutdated = true;
        Vector3 _averageVelocity;
        public Vector3 AverageVelocity
        {
            get
            {
                if (isAverageVelocityOutdated)
                {
                    _averageVelocity = CalculateAverageVelocity();
                    isAverageVelocityOutdated = false;
                }
                return _averageVelocity;
            }
        }

        public Vector3 VelocityOfLastStep { get => GetPosition(0) - GetPosition(-1); }

        Vector3 GetPosition(int relativeIndex)
        {
            int index = (currentIndex - 1 + relativeIndex);
            while (index < 0) index += recentPositions.Length;
            index = index  % recentPositions.Length;
            
            return recentPositions[index];
        }

        public void OnEnable()
        {
            recentPositions = new Vector3[movingAverageStepCount];
            ResetRecord();
        }

        public void FixedUpdate()
        {
            recentPositions[currentIndex] = GetPosition();
            currentIndex = (currentIndex + 1) % recentPositions.Length;
            isAverageVelocityOutdated = true;
        }

        public void ResetRecord()
        {
            for (int i = 0; i < recentPositions.Length; ++i)
            {
                recentPositions[i] = GetPosition();
            }
        }

        /// <summary>
        /// Produce a string representation of recent positions, intended for debug.
        /// </summary>
        public string DumpRecentPositions()
        {
            string str = "";
            for (int i = 0; i < recentPositions.Length; ++i)
            {
                str += $"{i.ToString("D2")}: {recentPositions[i].ToString("F3")}\n";
            }
            return str;
        }

        protected virtual Vector3 GetPosition()
        {
            return transform.position;
        }

        protected virtual Vector3 CalculateAverageVelocity()
        {
            int oldestIndex = currentIndex;
            int newestIndex = (currentIndex + recentPositions.Length - 1) % recentPositions.Length;
            Vector3 velocity = (recentPositions[newestIndex] - recentPositions[oldestIndex]) / (recentPositions.Length * Time.fixedDeltaTime);
            return velocity;
        }
    }
}

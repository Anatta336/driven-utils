using UnityEngine;

namespace SamDriver.Util
{
    public class RecentRotation
    {
        public int Length { get; private set; }
        Quaternion[] rotations;
        int nextRotationIndex = 0;

        public RecentRotation(int length, Quaternion initialRotation)
        { 
            this.Length = length;
            rotations = new Quaternion[this.Length];
            Fill(initialRotation);
        }

        public void Store(Quaternion rotation)
        {
            rotations[nextRotationIndex] = rotation;
            nextRotationIndex = (nextRotationIndex + 1) % Length;
        }

        public float AngleBetweenOldestNewest()
        {
            int oldestIndex = nextRotationIndex;
            int newestIndex = (nextRotationIndex + rotations.Length - 1) % rotations.Length;
            Quaternion oldestRotation = rotations[oldestIndex];
            Quaternion newestRotation = rotations[newestIndex];

            return Quaternion.Angle(oldestRotation, newestRotation);
        }

        public void Fill(Quaternion filler)
        {
            for (int i = 0; i < rotations.Length; ++i)
            {
                rotations[i] = filler;
            }
        }
    }
}

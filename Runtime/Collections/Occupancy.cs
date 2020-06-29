using UnityEngine;

namespace SamDriver.Util
{
    public class Occupancy
    {
        public readonly object OccupancyLock = new object();

        public int Length {get; private set; }

        byte[] bytes;
        int byteCount;
        
        const int bitsPerByte = 8;
        const byte fullByte = byte.MaxValue;

        public Occupancy(int length_)
        {
            Length = length_;
            byteCount = Mathf.CeilToInt((float)Length / bitsPerByte);
            bytes = new byte[byteCount];
        }

        /// <returns>Returns index of location that was reserved, or -1 if no space available.</returns>
        public int TryReserveSpace()
        {
            lock (OccupancyLock)
            {
                for (int byteIndex = 0; byteIndex < byteCount; ++byteIndex)
                {
                    byte b = bytes[byteIndex];
                    // Debug.Log("Checking byte: " + Convert.ToString(b, 2).PadLeft(8, '0'));

                    if (IsByteFull(b))
                    {
                        // Debug.Log("Byte is full.");
                        continue;
                    }

                    int bitIndexInByte = TryReserveInByte(ref b);
                    if (bitIndexInByte == -1) continue;

                    bytes[byteIndex] = b;
                    return byteIndex * bitsPerByte + bitIndexInByte;
                }
                return -1;
            }
        }

        public void FreeSpace(int index)
        {
            int byteIndex = index / bitsPerByte; // integer division, discarding remainder
            int bitIndexWithinByte = index % bitsPerByte;

            lock (OccupancyLock)
            {
                byte b = bytes[byteIndex];
                SetBitInByte(ref b, bitIndexWithinByte, false);
                bytes[byteIndex] = b;
            }
        }

        public bool HasSpace()
        {
            lock (OccupancyLock)
            {
                for (int byteIndex = 0; byteIndex < byteCount; ++byteIndex)
                {
                    if (!IsByteFull(bytes[byteIndex]))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool HasSpace(int spacesRequired)
        {
            int remaining = spacesRequired;
            lock (OccupancyLock)
            {
                for (int byteIndex = 0; byteIndex < byteCount; ++byteIndex)
                {
                    byte b = bytes[byteIndex];
                    if (IsByteFull(b)) continue;

                    for (int bitIndex = 0; bitIndex < bitsPerByte; ++bitIndex)
                    {
                        if (!GetBitInByte(b, bitIndex))
                        {
                            --remaining;
                            if (remaining == 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        bool IsByteFull(byte value)
        {
            return value == fullByte;
        }

        /// <returns>Returns offset within byte of bit that was set to true, or -1 if no space within byte.</returns>
        int TryReserveInByte(ref byte source)
        {
            for (int bitIndex = 0; bitIndex < bitsPerByte; ++bitIndex)
            {
                bool bit = GetBitInByte(source, bitIndex);
                if (!bit)
                {
                    SetBitInByte(ref source, bitIndex, true);
                    return bitIndex;
                }
            }
            return -1;
        }

        bool GetBitInByte(byte source, int offset)
        {
            int mask = 0b_0000_0001 << offset;
            return (source & mask) != 0;
        }

        void SetBitInByte(ref byte source, int offset, bool value)
        {
            // Debug.Log($"Setting bit {offset} to {value} in byte: " + Convert.ToString(source, 2).PadLeft(8, '0'));
            int mask = 0b_0000_0001 << offset;
            if (value)
            {
                source = (byte)(source | mask);
            }
            else
            {
                source = (byte)(source & ~mask);
            }
            // Debug.Log("Resulting byte: " + Convert.ToString(source, 2).PadLeft(8, '0'));
        }
    }
}

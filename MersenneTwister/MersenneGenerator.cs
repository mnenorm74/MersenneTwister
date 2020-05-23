using System;
using System.Collections.Generic;

namespace MersenneTwister
{
    public class MersenneGenerator
    {
        private const int p = 624;
        private const int q = 397;
        private const int u = 11;
        private const int s = 7;
        private const int t = 15;
        private const int l = 18;
        private const ulong b = 0x9d2c5680UL;
        private const ulong a = 0x9908b0dfUL;
        private const ulong c = 0xefc60000UL;
        private const ulong upperMask = 0x80000000UL;
        private const ulong lowerMask = 0x7fffffffUL;
        private readonly ulong[] x = new ulong[p];
        private int index = p + 1;

        public MersenneGenerator()
        {
            InitializeRandom(new ulong[] {0x123, 0x234, 0x345, 0x456});
        }

        public double GetRealNumber(double minValue, double maxValue)
        {
            return GetUint32() * ((maxValue - minValue) / 4294967295.0) + minValue;
        }

        public int GetIntegerNumber(int minValue, int maxValue)
        {
            return (int) GetRealNumber(minValue, maxValue);
        }

        public IEnumerable<double> GetRealNumbers(double minValue, double maxValue, int count)
        {
            var numbers = new List<double>();
            for (var i = 0; i < count; i++)
            {
                numbers.Add(GetRealNumber(minValue, maxValue));
            }

            return numbers;
        }

        public IEnumerable<int> GetIntegerNumbers(int minValue, int maxValue, int count)
        {
            var numbers = new List<int>();
            for (var i = 0; i < count; i++)
            {
                numbers.Add(GetIntegerNumber(minValue, maxValue));
            }

            return numbers;
        }

        private void InitializeRandom(ulong s)
        {
            x[0] = s & 0xffffffffUL;
            for (index = 1; index < p; index++)
            {
                x[index] = 1812433253UL * (x[index - 1] ^ (x[index - 1] >> 30)) + (ulong) index;
                x[index] &= 0xffffffffUL;
            }
        }

        private void InitializeRandom(ulong[] init_key)
        {
            InitializeRandom(19650218UL);
            var i = 1;
            var j = 0;
            var k = p > init_key.Length ? p : init_key.Length;
            while (k != 0)
            {
                x[i] = (x[i] ^ ((x[i - 1] ^ (x[i - 1] >> 30)) * 1664525UL)) + init_key[j] + (ulong) j;
                x[i] &= 0xffffffffUL;
                i++;
                j++;
                if (i >= p)
                {
                    x[0] = x[p - 1];
                    i = 1;
                }

                if (j >= init_key.Length)
                {
                    j = 0;
                }

                k--;
            }

            for (k = p - 1; k != 0; k--)
            {
                x[i] = (x[i] ^ ((x[i - 1] ^ (x[i - 1] >> 30)) * 1566083941UL)) - (ulong) i; 
                x[i] &= 0xffffffffUL;
                i++;
                if (i >= p)
                {
                    x[0] = x[p - 1];
                    i = 1;
                }
            }

            x[0] = 0x80000000UL; 
        }

        private ulong GetUint32()
        {
            ulong[] mag01 = {0x0UL, a};
            ulong y;
            if (index >= p)
            {
                int kk;
                if (index == p + 1)
                {
                    InitializeRandom(5489UL);
                }

                for (kk = 0; kk < p - q; kk++)
                {
                    y = (x[kk] & upperMask) | (x[kk + 1] & lowerMask);
                    x[kk] = x[kk + q] ^ (y >> 1) ^ mag01[y & 0x1UL];
                }

                while (kk < p - 1)
                {
                    y = (x[kk] & upperMask) | (x[kk + 1] & lowerMask);
                    x[kk] = x[kk + (q - p)] ^ (y >> 1) ^ mag01[y & 0x1UL];
                    kk++;
                }

                y = (x[p - 1] & upperMask) | (x[0] & lowerMask);
                x[p - 1] = x[q - 1] ^ (y >> 1) ^ mag01[y & 0x1UL];
                index = 0;
            }

            y = x[index++];
            y ^= (y >> u);
            y ^= (y << s) & b;
            y ^= (y << t) & c;
            y ^= (y >> l);
            return y;
        }
    }
}
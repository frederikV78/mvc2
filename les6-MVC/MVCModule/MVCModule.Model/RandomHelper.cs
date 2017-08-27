using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVCModule.Models {
    internal static class RandomHelper {
        public static int Next() {
            return BitConverter.ToInt32(NextBytes(4), 0);
        }
        public static int Next(int max) {
            if (max < 0)
                throw new ArgumentOutOfRangeException("maxValue");
            return (int)(NextDouble() * (double)max);
        }
        public static int Next(int min, int max) {
            if (min > max)
                throw new ArgumentOutOfRangeException("min");
            long num = (long)max - (long)min;
            if (num <= 2147483647L) {
                return (int)(NextDouble() * (double)num) + min;
            }
            return (int)((long)(NextDouble() * (double)num) + (long)min);
        }
        public static double NextDouble() {
            int sample = Next();
            if (sample == int.MaxValue) {
                sample--;
            } else if (sample < 0) {
                sample += int.MaxValue;
            }
            return (double)sample * 4.6566128752457969E-10;
            /*// Step 1: fill an array with 8 random bytes
            var bytes = new Byte[8];
            Instance.GetBytes(bytes);
            // Step 2: bit-shift 11 and 53 based on double's mantissa bits
            var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
            return ul / (Double)(1UL << 53);*/
        }
        public static bool NextBool() {
            return NextDouble() >= 0.5;
        }
        public static byte[] NextBytes(int count) {
            byte[] buffer = new byte[count];
            NextBytes(buffer);
            return buffer;
        }
        public static void NextBytes(byte[] bytes) {
            Instance.GetBytes(bytes); // thread safe
        }
        public static T Choose<T>(this IList<T> bla) {
            return bla[Next(bla.Count)];
        }
        public static void Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private static RNGCryptoServiceProvider Instance {
            get {
                if (_instance == null)
                    _instance = new RNGCryptoServiceProvider();
                return _instance;
            }
        }

        private static RNGCryptoServiceProvider _instance;
    }
}

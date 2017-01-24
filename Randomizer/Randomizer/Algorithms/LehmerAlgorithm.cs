using Randomizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.Algorithms
{
    public class LehmerAlgorithm : IAlgorithm
    {
        private const int a = 16807;
        private const int m = 2147483647;
        private const int q = 127773;
        private const int r = 2836;
        private int seed;

        public LehmerAlgorithm(int seed)
        {
            if (seed <= 0 || seed == int.MaxValue)
                throw new Exception("Bad seed");
            this.seed = seed;
        }

        public double Next()
        {
            int hi = seed / q;
            int lo = seed % q;
            seed = (a * lo) - (r * hi);
            if (seed <= 0)
                seed = seed + m;
            return (seed * 1.0) / m;
        }

        public int Next(int to) => (int)(to * Next());  // [0, to]

        public int Next(int from, int to) => (int)((to - from) * Next() + from); // [from, to]
    }
}

using Randomizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.Algorithms
{
    public class LinearCongruentialAlgorithm : IAlgorithm
    {
        private const long a = 25214903917;
        private const long c = 11;
        private long seed;

        public LinearCongruentialAlgorithm(long seed)
        {
            if (seed < 0)
                throw new Exception("Bad seed");
            this.seed = seed;
        }

        private int next(int bits) // helper
        {
            seed = (seed * a + c) & ((1L << 48) - 1);
            return (int)(seed >> (48 - bits));
        }

        public double Next()
        {
            return (((long)next(26) << 27) + next(27)) / (double)(1L << 53);
        }

        public int Next(int to) => (int)(to * Next());  // [0, to]

        public int Next(int from, int to) => (int)((to - from) * Next() + from); // [from, to]
    }
}

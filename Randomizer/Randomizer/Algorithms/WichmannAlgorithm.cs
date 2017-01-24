using Randomizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.Algorithms
{
    public class WichmannAlgorithm : IAlgorithm
    {
        private int s1, s2, s3;

        public WichmannAlgorithm(int seed)
        {
            if (seed <= 0 || seed > 30000)
                throw new Exception("Bad seed");
            s1 = seed;
            s2 = seed + 1;
            s3 = seed + 2;
        }

        public double Next()
        {
            s1 = 171 * (s1 % 177) - 2 * (s1 / 177);
            if (s1 < 0) { s1 += 30269; }
            s2 = 172 * (s2 % 176) - 35 * (s2 / 176);
            if (s2 < 0) { s2 += 30307; }
            s3 = 170 * (s3 % 178) - 63 * (s3 / 178);
            if (s3 < 0) { s3 += 30323; }
            double r = (s1 * 1.0) / 30269 + (s2 * 1.0) / 30307 + (s3 * 1.0) / 30323;
            return r - Math.Truncate(r);  // orig uses % 1.0
        }

        public int Next(int to) => (int)(to * Next());  // [0, to]

        public int Next(int from, int to) => (int)((to - from) * Next() + from); // [from, to]
    }
}

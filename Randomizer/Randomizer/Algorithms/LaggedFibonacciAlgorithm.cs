﻿using Randomizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Randomizer.Algorithms
{
    public class LaggedFibonacciAlgorithm : IAlgorithm
    {
        private const int k = 10; // Largest magnitude"-index"
        private const int j = 7; // Other "-index"
        private const int m = 2147483647;  // 2^31 - 1 = maxint
        private List<int> vals = null;
        private int seed;

        public LaggedFibonacciAlgorithm(int seed)
        {
            vals = new List<int>();
            for (int i = 0; i < k + 1; ++i)
                vals.Add(seed);
            if (seed % 2 == 0) vals[0] = 11;
            // Burn some values away
            for (int ct = 0; ct < 1000; ++ct)
            {
                double dummy = this.Next();
            }
        }  // ctor

        public double Next()
        {
            // (a + b) mod n = [(a mod n) + (b mod n)] mod n
            int left = vals[0] % m;    // [x-big]
            int right = vals[k - j] % m; // [x-other]
            long sum = (long)left + (long)right;  // prevent overflow
            seed = (int)(sum % m);  // Because m is int, result has int range
            vals.Insert(k + 1, seed);  // Add new val at end
            vals.RemoveAt(0);  // Delete now irrelevant [0] val
            return (1.0 * seed) / m;
        }

        public int Next(int to) => (int)(to* Next());  // [0, to]

        public int Next(int from, int to) => (int)((to - from) * Next() + from); // [from, to]
    }
}

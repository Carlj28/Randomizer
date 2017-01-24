using Randomizer.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomizerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            LehmerAlgorithm lehmerAlgorithm = new LehmerAlgorithm(100);

            for(int i = 0; i < 100; i++)
            {
                Console.Write(lehmerAlgorithm.Next(100)+ " ");
            }

            Console.ReadKey();

            WichmannAlgorithm wichmannAlgorithm = new WichmannAlgorithm(100);

            for (int i = 0; i < 100; i++)
            {
                Console.Write(wichmannAlgorithm.Next(100) + " ");
            }

            Console.ReadKey();
        }
    }
}

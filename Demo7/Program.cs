using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (int i in GetData())
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }

        public static IEnumerable<int> GetData()
        {
            Thread.Sleep(1000);
            yield return 1;

            Thread.Sleep(1000);
            yield return 2;

            Thread.Sleep(1000);
            yield return 3;
        }
    }
}

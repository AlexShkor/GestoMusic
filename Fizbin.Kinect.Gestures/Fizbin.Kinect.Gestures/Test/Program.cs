using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GestoMusic;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var samplesFactory = new SamplesFactory();
            var kalimba = samplesFactory.GetMusic();

            kalimba.Play(1.0);
            
            Console.ReadLine();
        }
    }
}

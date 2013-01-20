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
            var kalimba = samplesFactory.Wawe();
            var drum = samplesFactory.GetDrum();
            var guitar = samplesFactory.GetGitare();

            //kalimba.Play(1.0);
            //kalimba.PlayNonStop(1);

            guitar.PlayNonStop();

            //while (true)
            //{
            //    Thread.Sleep(3000);
            //    guitar.Faster();
            //    drum.Play(1);
            //}
            
            Console.ReadLine();
        }
    }
}

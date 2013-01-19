using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;

namespace MusicPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new MusicPlayer();

            player.Clap();

            Console.ReadLine();
        }
    }
}

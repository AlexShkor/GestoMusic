using System;
using System.IO;
using System.Media;
using System.Security.AccessControl;

namespace GestoMusic
{
    public class Sample
    {
        private readonly string _sample;

        public Sample(string sample)
        {
            _sample = sample;

           
        }

        public void Play()
        {
            using (var _audio = new FileStream(_sample, FileMode.Open,FileAccess.Read,FileShare.Read))
            {
                
            var player = new SoundPlayer(_audio);
            player.Play();
            }
        }

    }
}
using System;
using System.IO;
using System.Media;

namespace GestoMusic
{
    public class Sample : IDisposable
    {
        private readonly string _sample;
        private readonly FileStream _audio;

        public Sample(string sample)
        {
            _sample = sample;

            _audio = new FileStream(_sample, FileMode.Open);
        }

        public void Play()
        {
            var player = new SoundPlayer(_audio);
            player.Play();
        }

        public void Dispose()
        {
            _audio.Dispose();
        }
    }
}
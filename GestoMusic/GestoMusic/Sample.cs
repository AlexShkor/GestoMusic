using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;

namespace GestoMusic
{
    public class Sample : IDisposable
    {
        private readonly string _sample;
        private readonly FileStream _audio;

        public Sample(string sample)
        {
            _sample = sample;
        }

        public void Play(double rate)
        {
            var player = new WMPLib.WindowsMediaPlayer();
            player.URL = _sample;
            player.settings.rate = rate;
        }

        public void Dispose()
        {
            _audio.Dispose();
        }
    }
}
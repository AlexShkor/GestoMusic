using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;

namespace MusicPlayer
{
    class MusicPlayer
    {
        public void Clap()
        {
            PlaySample("./Samples/808-clap.wav");
        }

        private void PlaySample(string sample)
        {
            using (var audio = new FileStream(sample, FileMode.Open))
            {
                var player = new SoundPlayer(audio);
                player.Play();
            }
        }
    }
}

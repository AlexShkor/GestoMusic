using System;
using NAudio.Wave;
using WMPLib;

namespace GestoMusic
{
    public class Sample
    {
        private readonly string _sample;
        private readonly WaveOut _player = new WaveOut();

        public Sample(string sample)
        {
            _sample = sample;
        }

        public double Pitch { get; set; }

        public void Play()
        {
            PlayWithNAudio();
        }

        private void PlayWithNAudio()
        {
            var waveStream = new WaveFileReader(_sample);

            _player.Init(waveStream);

            _player.Volume = 1.0f;
            _player.Play();
        }

        public void PlayNonStop()
        {
            var reader = new WaveFileReader(_sample);
            var loop = new LoopStream(reader);

            var wave = new WaveOut();
            wave.Init(loop);
            wave.Play();
        }

        public void Stop()
        {
            _player.Pause();
        }
    }
}
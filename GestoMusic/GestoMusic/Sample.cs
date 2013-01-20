using System;
using System.IO;
using JSNet;
using NAudio.Wave;
using WMPLib;

namespace GestoMusic
{
    public class Sample
    {
        private readonly string _sample;
        private SuperPitch _pitch;
        private WaveOut _wave;
        private WaveFileReader _waveStream;

        public Sample(string sample)
        {
            _sample = sample;
        }

        public float Pitch
        {
            set
            {
                _pitch.SetSemitones(value);
                _pitch.Slider();
            }
        }

        public void Play()
        {
            PlayWithNAudio();
        }

        private void PlayWithNAudio()
        {
            if (_waveStream == null)
            {
                _waveStream = new WaveFileReader(_sample);


            }
            else
            {
                _waveStream.Seek(0, SeekOrigin.Begin);

            }
            _wave = new WaveOut();
            _wave.Init(_waveStream);

            _wave.Volume = 1.0f;
            _wave.Play();
        }

        public void PlayNonStop()
        {
            if (_wave == null)
            {
                var reader = new WaveFileReader(_sample);
                var loop = new LoopStream(reader);
                var effects = new EffectChain();
                var effectStream = new EffectStream(effects, loop);
                _pitch = new SuperPitch();
                effectStream.AddEffect(_pitch);
                _wave = new WaveOut();
                _wave.Init(effectStream);
                _wave.Play();
            }
            else
            {
                _wave.Resume();
            }
        }

        public void Stop()
        {
            _wave.Pause();
        }
    }
}
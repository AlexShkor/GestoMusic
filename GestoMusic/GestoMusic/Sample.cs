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
        private IWaveProvider _waveStream;

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

        public IWaveProvider LoadFile(string fileName)
        {
            IWaveProvider waveStream = null;
            if (fileName.EndsWith(".mp3"))
            {
                waveStream = new Mp3FileReader(fileName);
            }
            else if (fileName.EndsWith(".wav"))
            {
                waveStream = new WaveFileReader(fileName);
            }
            return waveStream;
        }


        private void PlayWithNAudio()
        {
            var stream =LoadFile(_sample);
            var wave = new WaveOut();
            wave.Init(stream);

            wave.Volume = 1.0f;
            wave.Play();
        }

        private void SeekToBegin()
        {
            if (_waveStream is Mp3FileReader)
            {
                (_waveStream as Mp3FileReader).Seek(0, SeekOrigin.Begin);
            }
            if (_waveStream is WaveFileReader)
            {
                (_waveStream as WaveFileReader).Seek(0, SeekOrigin.Begin);
            }
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
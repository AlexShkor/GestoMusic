﻿using System;
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

        public bool IsConcurent { get; set; }

        private bool _isPlaying = false;
        private DirectSoundOut _player;
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
            if (!IsConcurent || _waveStream == null || _waveStream.Position == _waveStream.Length)
            {
                _waveStream = new WaveFileReader(_sample);
                var superWavStream32 = processWaveStream(_waveStream);

                _player = new DirectSoundOut(50);
                _player.Init(superWavStream32);

                _player.Volume = 1.0f;
                _player.Play();
            }
        }

        private WaveChannel32 processWaveStream(WaveStream readerStream)
        {
            // Provide PCM conversion if needed
            if (readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
            {
                readerStream = WaveFormatConversionStream.CreatePcmStream(readerStream);
                readerStream = new BlockAlignReductionStream(readerStream);
            }

            // Provide conversion to 16 bits if needed
            if (readerStream.WaveFormat.BitsPerSample != 16)
            {
                var format = new WaveFormat(readerStream.WaveFormat.SampleRate,
                16, readerStream.WaveFormat.Channels);
                readerStream = new WaveFormatConversionStream(format, readerStream);
            }

            var inputStream = new WaveChannel32(readerStream);

            return inputStream;
        }

        public void PlayNonStop()
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

        public void Stop()
        {
            if (_wave != null)
            {
                _wave.Pause();
            }
        }
    }
}
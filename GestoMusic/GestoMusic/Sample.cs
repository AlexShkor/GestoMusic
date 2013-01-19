using System;
using NAudio.Wave;
using WMPLib;

namespace GestoMusic
{
    public class Sample
    {
        private readonly string _sample;
        private readonly WindowsMediaPlayer _player;

        public Sample(string sample)
        {
            _sample = sample;
            _player = new WindowsMediaPlayer();
        }

        public void Play(double rate = 1d)
        {
            _player.URL = _sample;
            
            _player.settings.volume = 100;
        }

        public void PlayWithNAudio()
        {
            var waveOutDevice = new DirectSoundOut(50);

            var mainOutputStream = CreateInputStream(_sample);
        }

        private static WaveStream CreateInputStream(string fileName)
        {
            WaveStream readerStream = null;

            if (fileName.EndsWith(".wav"))
            {
                readerStream = new WaveFileReader(fileName);
            }
            else
            {
                throw new InvalidOperationException("Unsupported extension");
            }

            // Provide PCM conversion if needed
            if (readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
            {
                readerStream = WaveFormatConversionStream.CreatePcmStream(readerStream);
                readerStream = new BlockAlignReductionStream(readerStream);
            }

            // Provide conversion to 16 bits if needed
            if (readerStream.WaveFormat.BitsPerSample != 16)
            {
                var format = new WaveFormat(readerStream.WaveFormat.SampleRate, 16, readerStream.WaveFormat.Channels);
                readerStream = new WaveFormatConversionStream(format, readerStream);
            }

            var inputStream = new WaveChannel32(readerStream);

            return inputStream;
        }


        public void PlayNonStop(double rate)
        {
            Play(rate);
            _player.settings.setMode("loop", true);
        }

        public void Slower()
        {
            _player.settings.rate -= 0.2;
        }

        public void Faster()
        {
            _player.settings.rate += 0.2;
        }
    }
}
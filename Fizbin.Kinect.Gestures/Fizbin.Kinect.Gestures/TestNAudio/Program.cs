﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NAudio.Wave;

namespace TestNAudio
{
    class Program
    {
        static void Main(string[] args)
        {
            IWavePlayer waveOutDevice;
            WaveStream mainOutputStream;
            string fileName = @"C:\Users\kuna\Documents\GitHub\GestoMusic\GestoMusic\GestoMusic\Samples\Guitar.wav";

            Console.WriteLine("Initiailizing NAudio");
            try
            {
                waveOutDevice = new DirectSoundOut(50);
            }
            catch (Exception driverCreateException)
            {
                    Console.WriteLine(String.Format("{0}", driverCreateException.Message));
                return; 
            }

            mainOutputStream = CreateInputStream(fileName);
            try
            {
                waveOutDevice.Init(mainOutputStream);
            }
            catch (Exception initException)
            {
                Console.WriteLine(String.Format("{0}", initException.Message), "Error Initializing Output");
                return;
            }

            Console.WriteLine("NAudio Total Time: " + (mainOutputStream as WaveChannel32).TotalTime);

            Console.WriteLine("Playing WMA..");

            waveOutDevice.Volume = 1.0f;
            waveOutDevice.Play();

            Console.ReadKey();

            Console.WriteLine("Seeking to new time: 00:00:20..");

            //(mainOutputStream as WaveChannel32).CurrentTime = new TimeSpan(0, 0, 20);

            Console.ReadKey();

            Console.WriteLine("Hit key to stop..");

            waveOutDevice.Stop();

            Console.WriteLine("Finished..");

            mainOutputStream.Dispose();

            waveOutDevice.Dispose();

            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }

        private static WaveStream CreateInputStream(string fileName)
        {
            WaveChannel32 inputStream;
            WaveStream readerStream = null;

            if (fileName.EndsWith(".wav"))
            {
                readerStream = new WaveFileReader(fileName);
            }
            //else if (fileName.EndsWith(".wma"))
            //{
            //    //                readerStream = new WMAFileReader2(fileName);

            //    MemoryStream memoryStream = new MemoryStream();
            //    using (FileStream infile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            //    {
            //        while (infile.Position < infile.Length)
            //        {
            //            byte data = (byte)infile.ReadByte();
            //            memoryStream.WriteByte(data);
            //        }
            //    }
            //    memoryStream.Position = 0;
            //    readerStream = new WMAFileReader2(memoryStream);
            //}
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
                var format = new WaveFormat(readerStream.WaveFormat.SampleRate,
                16, readerStream.WaveFormat.Channels);
                readerStream = new WaveFormatConversionStream(format, readerStream);
            }

            inputStream = new WaveChannel32(readerStream);

            return inputStream;
        }
    }
}

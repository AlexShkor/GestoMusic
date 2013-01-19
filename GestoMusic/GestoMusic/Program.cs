using System;
using System.Threading;
using Fizbin.Kinect.Gestures;

namespace GestoMusic
{
    public class Program
    {
        public static void Main()
        {
            var gesturesObserver = new GesturesObserver();
            var samplesFactory = new SamplesFactory();
            var gitare = samplesFactory.GetGitare();
            var plate = samplesFactory.GetGitare();
            var tube = samplesFactory.GetGitare();
            var drum = samplesFactory.GetGitare();
            var metronom = samplesFactory.GetMetronom();

            //gesturesObserver.TrackGesture(GestureType.SwipeLeft, gitare);
            //gesturesObserver.TrackGesture(GestureType.SwipeRight, plate);
            //gesturesObserver.TrackGesture(GestureType.WaveLeft, tube);
            //gesturesObserver.TrackGesture(GestureType.WaveRight, drum);

            metronom.Play(1);

            while (true)
            {
                Thread.Sleep(100);
            }
        }
    }
}
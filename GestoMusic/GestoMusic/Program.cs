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
            gesturesObserver.TrackDiscretGesture(GestureType.SwipeLeft, gitare);
            gesturesObserver.TrackDiscretGesture(GestureType.SwipeRight, plate);
            gesturesObserver.TrackDiscretGesture(GestureType.WaveLeft, tube);
            gesturesObserver.TrackDiscretGesture(GestureType.WaveRight, drum);
            while (true)
            {
                Thread.Sleep(100);
            }
        }
    }
}
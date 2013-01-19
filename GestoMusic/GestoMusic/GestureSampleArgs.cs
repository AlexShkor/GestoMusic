using System;
using Fizbin.Kinect.Gestures;

namespace GestoMusic
{
    public class GestureSampleArgs : EventArgs
    {
        public GestureEventArgs GestureEventArgs { get; set; }

        public Sample Sample { get; set; }

        public string Message { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Fizbin.Kinect.Gestures;

namespace GestoMusic
{
    public class GesturesObserver
    {
        private readonly Dictionary<GestureType, Sample> _getsturesActions = new Dictionary<GestureType, Sample>();
        private readonly GestureController _gestureController;

        public GesturesObserver()
        {
            _gestureController = new GestureController();
            _gestureController.GestureRecognized += OnGestureRecognized;
        }

        private void OnGestureRecognized(object sender, GestureEventArgs e)
        {
            if (_getsturesActions.ContainsKey(e.GestureType))
            {
                var sample = _getsturesActions[e.GestureType];
                sample.Play();
                Console.WriteLine("{0} recognized. {1} is playing.", e.GestureType, sample);
            }
        }

        public void TrackGesture(GestureType rightHand, Sample sample)
        {
            _getsturesActions[rightHand] = sample;
        }

        public void Update()
        {
            throw new NotImplementedException();

        }
    }
}
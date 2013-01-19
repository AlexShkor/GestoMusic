using System;
using System.Collections.Generic;
using Fizbin.Kinect.Gestures;

namespace GestoMusic
{
    public class GesturesObserver
    {
        private readonly Dictionary<GestureType, Sample> _getsturesActions = new Dictionary<GestureType, Sample>();
        private readonly GestureController _gestureController;
        public event EventHandler<GestureSampleArgs> GestureSamplePlayed;


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
                var message = string.Format("{0} recognized. {1} is playing.", e.GestureType, sample);
                if (this.GestureSamplePlayed != null)
            {
                this.GestureSamplePlayed(this, new GestureSampleArgs
                    {
                        GestureEventArgs = e,
                        Sample = sample,
                        Message = message
                    });
            }


            }
        }

        public void TrackGesture(GestureType rightHand, Sample sample)
        {
            _getsturesActions[rightHand] = sample;
        }
    }

    public class GestureSampleArgs : EventArgs
    {
        public GestureEventArgs GestureEventArgs { get; set; }

        public Sample Sample { get; set; }

        public string Message { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect;

namespace GestoMusic
{
    public class GesturesObserver
    {
        private readonly Dictionary<GestureType, Sample> _getsturesActions = new Dictionary<GestureType, Sample>();
        private readonly Dictionary<ContinuesGestureSettings, Sample> _continuesGestures = new Dictionary<ContinuesGestureSettings, Sample>();
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

        public void TrackDiscretGesture(GestureType rightHand, Sample sample)
        {
            _getsturesActions[rightHand] = sample;
        }

        public void TrackContinuesGesture(ContinuesGestureSettings settings, Sample sample)
        {
            
        }

        public void UpdateAllGestures(Skeleton skeleton)
        {
            _gestureController.UpdateAllGestures(skeleton);
            foreach (var continuesGesture in _continuesGestures)
            {
                var settings = continuesGesture.Key;
                var sample = continuesGesture.Value;
                if (continuesGesture.Key.IsTracked)
                {
                    settings.OnSkeletonAdjustment(new SkeletonArgs
                        {
                            Skeleton = skeleton
                        });
                    settings.Play(sample);
                }
            }
        }
    }
}
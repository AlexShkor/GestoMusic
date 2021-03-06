using System;
using System.Collections.Generic;
using System.Linq;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect;

namespace GestoMusic
{
    public class ContinuesGestureSettings
    {

        protected IEnumerable<GestureType> Empty = Enumerable.Empty<GestureType>();
        protected IEnumerable<GestureType> ExcludeList = Enumerable.Empty<GestureType>();
 

        public float Volume { get; set; }
        public float Pitch { get; set; }
        public float Speed { get; set; }

        public IEnumerable<GestureType> ExcludeGesturesIfTracked
        {
            get
            {
                return !IsTracked ? Empty : ExcludeList;
            }
        }

        public GestureType ActivationGesture { get; set; }
        public GestureType DeactivationGesture { get; set; }

        public ContinuesGestureSettings()
        {
            Volume = 1f;
            Pitch = 0f;
            Speed = 1f;
        }

        private readonly ContinuesMusicPlayer _continuesMusucPlayer = new ContinuesMusicPlayer();
        public event EventHandler<SkeletonArgs> SkeletonAdjustment;

        public void OnSkeletonAdjustment(SkeletonArgs e)
        {
            EventHandler<SkeletonArgs> handler = SkeletonAdjustment;
            if (handler != null) handler(this, e);
            _continuesMusucPlayer.SetPitch(Pitch);
        }

        public bool IsTracked { get; protected set; }

        public Sample Sample
        {
            get { return _continuesMusucPlayer.Sample; }
            set { _continuesMusucPlayer.Sample = value; }
        }

        private void Start()
        {
            _continuesMusucPlayer.Start();
        }

        private void Pause()
        {
            _continuesMusucPlayer.Pause();
        }

        public void Update(GestureType gestureType)
        {
            var newStatus = GetActiveStatus(gestureType);
            switch (newStatus)
            {
                case ContinuesGestureResult.Activate:
                    if (!IsTracked)
                    {
                        Start();
                        IsTracked = true;
                    } 
                    break;
                case ContinuesGestureResult.Deactivate:
                    if (IsTracked)
                    {
                        Pause();
                        IsTracked = false;
                    }
                    break;
            }
        }

        protected virtual ContinuesGestureResult GetActiveStatus(GestureType gestureType)
        {
            if (ActivationGesture == gestureType)
            {
                return ContinuesGestureResult.Activate;
            }
            if (DeactivationGesture == gestureType)
            {
                return ContinuesGestureResult.Deactivate;
            }
            return ContinuesGestureResult.None;
        }

        public void Deactivate()
        {
            Sample.Stop();
            IsTracked = false;
            SkeletonAdjustment = null;
        }
    }

    public enum ContinuesGestureResult
    {
        None,
        Activate,
        Deactivate
    }

    public class SkeletonArgs : EventArgs
    {
        public Skeleton Skeleton { get; set; }
    }

    public class ContinuesMusicPlayer
    {
        public Sample Sample { get; set; }

        public void SetPitch(float pitch)
        {
            Sample.Pitch = pitch;
        }

        public void Start()
        {
            Sample.PlayNonStop();
        }

        public void Pause()
        {
            Sample.Stop();
        }
    }
}
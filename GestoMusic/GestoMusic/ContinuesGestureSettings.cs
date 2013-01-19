using System;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect;

namespace GestoMusic
{
    public class HandUpContiniousGestureSettings : ContinuesGestureSettings
    {
        private bool _isRightHandUp = false;
        private bool _isLeftHandUp = false;

        public HandUpContiniousGestureSettings()
        {
            SkeletonAdjustment += (sender, args) =>
                {

                    Pitch = (args.Skeleton.Joints[JointType.WristLeft].Position.Y -
                             args.Skeleton.Joints[JointType.WristRight].Position.Y) * Pitch;
                };
        }

    }

    public class ContinuesGestureSettings
    {
        public const float PitchDetla = 0.6f;

        public float Volume { get; set; }
        public float Pitch { get; set; }
        public float Speed { get; set; }

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

        public Sample Sample { get; set; }

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
                    } 
                    break;
                case ContinuesGestureResult.Deactivate:
                    if (IsTracked)
                    {
                        Pause();
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
        public void SetPitch(float pitch)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            

        }
    }
}
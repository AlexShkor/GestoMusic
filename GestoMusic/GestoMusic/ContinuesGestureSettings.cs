using System;
using System.Collections.Generic;
using System.Linq;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect;

namespace GestoMusic
{
    public class HandUpContiniousGestureSettings : ContinuesGestureSettings
    {

        public const float PitchDetla = 0.6f;
        private bool _isRightHandUp = false;
        private bool _isLeftHandUp = false;

        public HandUpContiniousGestureSettings()
        {
            SkeletonAdjustment += (sender, args) =>
                {

                    Pitch = (0.4f - (args.Skeleton.Joints[JointType.WristLeft].Position.X -
                             args.Skeleton.Joints[JointType.WristRight].Position.X) * PitchDetla);
                };
        }

        protected override ContinuesGestureResult GetActiveStatus(GestureType gestureType)
        {
            switch (gestureType)
            {
                case GestureType.DownHandLeft:
                    _isLeftHandUp = false;
                    break;
                case GestureType.DownHandRigth:
                    _isRightHandUp = false;
                    break;
                case GestureType.UpHandRight:
                    _isRightHandUp = true;
                    break;
                case GestureType.UpHandLeft:
                    _isLeftHandUp = true;
                    break;
                default:
                    return ContinuesGestureResult.None;
            }
            if (_isLeftHandUp && _isRightHandUp)
            {
                if (IsTracked)
                {
                    return ContinuesGestureResult.None;
                }
                else
                {
                    return ContinuesGestureResult.Activate;
                }
            }
            else
            {
                if (IsTracked)
                {
                    return ContinuesGestureResult.Deactivate;
                }
                else
                {
                    return ContinuesGestureResult.None;
                }
            }
            return ContinuesGestureResult.None;
        }

    }

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
        }

        public void Start()
        {
        }

        public void Pause()
        {

        }
    }
}
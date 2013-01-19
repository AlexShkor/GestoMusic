using System.Collections.Generic;
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
            ExcludeList = new List<GestureType>
               {
                   GestureType.HammerLeft,
                   GestureType.HammerRight
               };
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
                case GestureType.DownHandRight:
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
}
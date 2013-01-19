﻿using Microsoft.Kinect;

namespace Fizbin.Kinect.Gestures.Segments
{
    /// <summary>
    /// The second part of the swipe left gesture
    /// </summary>
    public class HammerRightSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            // Right elbow higher than shoulder
            if (skeleton.Joints[JointType.ElbowRight].Position.Y > skeleton.Joints[JointType.ShoulderRight].Position.Y)// && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
            {
                // Right wrist closer to kinect than elbow
                if (skeleton.Joints[JointType.WristRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z) //&& skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y)
                {
                    return GesturePartResult.Suceed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
    }
}
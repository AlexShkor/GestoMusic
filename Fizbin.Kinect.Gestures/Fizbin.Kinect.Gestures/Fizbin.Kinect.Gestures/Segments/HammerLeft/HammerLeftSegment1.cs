﻿using Microsoft.Kinect;

namespace Fizbin.Kinect.Gestures.Segments
{
    /// <summary>
    /// The second part of the swipe left gesture
    /// </summary>
    public class HammerLeftSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            // Left elbow higher than shoulder
            if (skeleton.Joints[JointType.ElbowLeft].Position.Y > skeleton.Joints[JointType.ShoulderLeft].Position.Y)// && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
            {
                // Left wrist closer to kinect than elbow
                if (skeleton.Joints[JointType.WristLeft].Position.Z > skeleton.Joints[JointType.ElbowLeft].Position.Z) //&& skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.HipCenter].Position.Y)
                {
                    return GesturePartResult.Suceed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
    }
}
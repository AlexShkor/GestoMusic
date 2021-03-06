﻿using Microsoft.Kinect;

namespace Fizbin.Kinect.Gestures.Segments
{
    /// <summary>
    /// The second part of the swipe left gesture
    /// </summary>
    public class StepRightSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            // Left elbow higher than shoulder
            if (skeleton.Joints[JointType.AnkleRight].Position.Y - skeleton.Joints[JointType.AnkleLeft].Position.Y < GestureParams.AnkleThresholdLow)
            {
                return GesturePartResult.Suceed;
            }            
            return GesturePartResult.Pausing;
        }
    }
}
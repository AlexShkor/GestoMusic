using Microsoft.Kinect;

namespace Fizbin.Kinect.Gestures.Segments
{
    /// <summary>
    /// The second part of the swipe left gesture
    /// </summary>
    public class HandUpRightSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            // Left elbow higher than shoulder
            if (skeleton.Joints[JointType.WristRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
            {
                return GesturePartResult.Suceed;
            }
            return GesturePartResult.Pausing;
        }
    }
}
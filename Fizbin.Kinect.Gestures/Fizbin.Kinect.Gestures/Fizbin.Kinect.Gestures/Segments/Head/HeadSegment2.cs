using Microsoft.Kinect;

namespace Fizbin.Kinect.Gestures.Segments
{
    /// <summary>
    /// The second part of the swipe left gesture
    /// </summary>
    public class HeadSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            // Left elbow higher than shoulder
            if (skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.Head].Position.Z > GestureParams.HeadThresholdForward)
            {
                return GesturePartResult.Suceed;
            }            
            return GesturePartResult.Pausing;
        }
    }
}
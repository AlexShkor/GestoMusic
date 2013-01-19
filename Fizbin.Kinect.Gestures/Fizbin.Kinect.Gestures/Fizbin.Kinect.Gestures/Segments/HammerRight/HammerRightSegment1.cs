using Microsoft.Kinect;

namespace Fizbin.Kinect.Gestures.Segments
{
    /// <summary>
    /// The second part of the swipe left gesture
    /// </summary>
    public class HammerRightSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton)
        {
            // Right elbow higher than shoulder
            if (skeleton.Joints[JointType.ElbowRight].Position.Y > skeleton.Joints[JointType.ShoulderRight].Position.Y - GestureParams.ElbowThreshold)
            {
                // Right wrist closer to kinect than elbow
                if (skeleton.Joints[JointType.WristRight].Position.Z > skeleton.Joints[JointType.ElbowRight].Position.Z)
                {
                    return GesturePartResult.Suceed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
    }
}
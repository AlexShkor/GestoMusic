using System;
using System.Collections.Generic;
using Fizbin.Kinect.Gestures.Segments;
using Microsoft.Kinect;

namespace Fizbin.Kinect.Gestures
{
    public class GestureController
    {
        /// <summary>
        /// The list of all gestures we are currently looking for
        /// </summary>
        private List<Gesture> gestures = new List<Gesture>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GestureController"/> class.
        /// </summary>
        public GestureController()
        {
            // Define the gestures

            IRelativeGestureSegment[] joinedhandsSegments = new IRelativeGestureSegment[20];
            JoinedHandsSegment1 joinedhandsSegment = new JoinedHandsSegment1();
            for (int i = 0; i < 20; i++)
            {
                // gesture consists of the same thing 10 times 
                joinedhandsSegments[i] = joinedhandsSegment;
            }
            AddGesture(GestureType.JoinedHands, joinedhandsSegments);

            IRelativeGestureSegment[] hammerRightSegments = new IRelativeGestureSegment[2];
            hammerRightSegments[0] = new HammerRightSegment1();
            hammerRightSegments[1] = new HammerRightSegment2();
            AddGesture(GestureType.HammerRight, hammerRightSegments);

            IRelativeGestureSegment[] hammerLeftSegments = new IRelativeGestureSegment[2];
            hammerLeftSegments[0] = new HammerLeftSegment1();
            hammerLeftSegments[1] = new HammerLeftSegment2();
            AddGesture(GestureType.HammerLeft, hammerLeftSegments);

            IRelativeGestureSegment[] stepLeftSegments = new IRelativeGestureSegment[2];
            stepLeftSegments[0] = new StepLeftSegment1();
            stepLeftSegments[1] = new StepLeftSegment2();
            AddGesture(GestureType.StepLeft, stepLeftSegments);

            IRelativeGestureSegment[] stepRightSegments = new IRelativeGestureSegment[2];
            stepRightSegments[0] = new StepRightSegment1();
            stepRightSegments[1] = new StepRightSegment2();
            AddGesture(GestureType.StepRight, stepRightSegments);

            IRelativeGestureSegment[] headSegments = new IRelativeGestureSegment[2];
            headSegments[0] = new HeadSegment1();
            headSegments[1] = new HeadSegment2();
            AddGesture(GestureType.Head, headSegments);

            IRelativeGestureSegment[] upHandLeftSegments = new IRelativeGestureSegment[2];
            upHandLeftSegments[0] = new HandUpLeftSegment1();
            upHandLeftSegments[1] = new HandUpLeftSegment2();
            AddGesture(GestureType.UpHandLeft, upHandLeftSegments);

            IRelativeGestureSegment[] upHandRightSegments = new IRelativeGestureSegment[2];
            upHandRightSegments[0] = new HandUpRightSegment1();
            upHandRightSegments[1] = new HandUpRightSegment2();
            AddGesture(GestureType.UpHandRight, upHandRightSegments);

            IRelativeGestureSegment[] downHandRightSegments = new IRelativeGestureSegment[2];
            downHandRightSegments[0] = new HandDownRightSegment1();
            downHandRightSegments[1] = new HandDownRightSegment2();
            AddGesture(GestureType.DownHandRight, downHandRightSegments);

            IRelativeGestureSegment[] downHandLeftSegments = new IRelativeGestureSegment[2];
            downHandLeftSegments[0] = new HandDownLeftSegment1();
            downHandLeftSegments[1] = new HandDownLeftSegment2();
            AddGesture(GestureType.DownHandLeft, downHandLeftSegments);


            //AddTwoSegmentGesture<HandDownLeftSegment1, HandDownLeftSegment2>(GestureType.DownHandLeft);
        }

        private void AddTwoSegmentGesture<TFirst,TSecond>(GestureType gestureType)
            where TFirst : IRelativeGestureSegment, new() where TSecond : IRelativeGestureSegment, new()
        {
            IRelativeGestureSegment[] segments = new IRelativeGestureSegment[2];
            segments[0] = new TFirst();
            segments[1] = new TSecond();
            AddGesture(gestureType, segments);
        }

        /// <summary>
        /// Occurs when [gesture recognised].
        /// </summary>
        public event EventHandler<GestureEventArgs> GestureRecognized;

        /// <summary>
        /// Updates all gestures.
        /// </summary>
        /// <param name="data">The skeleton data.</param>
        public void UpdateAllGestures(Skeleton data)
        {
            foreach (Gesture gesture in this.gestures)
            {
                gesture.UpdateGesture(data);
            }
        }

        /// <summary>
        /// Adds the gesture.
        /// </summary>
        /// <param name="type">The gesture type.</param>
        /// <param name="gestureDefinition">The gesture definition.</param>
        public void AddGesture(GestureType type, IRelativeGestureSegment[] gestureDefinition)
        {
            Gesture gesture = new Gesture(type, gestureDefinition);
            //gesture.GestureRecognized += new EventHandler<GestureEventArgs>(this.Gesture_GestureRecognized);
            gesture.GestureRecognized += OnGestureRecognized;
            this.gestures.Add(gesture);
        }

        /// <summary>
        /// Handles the GestureRecognized event of the g control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KinectSkeltonTracker.GestureEventArgs"/> instance containing the event data.</param>
        //private void Gesture_GestureRecognized(object sender, GestureEventArgs e)
        private void OnGestureRecognized(object sender, GestureEventArgs e)
        {
            if (this.GestureRecognized != null)
            {
                this.GestureRecognized(this, e);
            }

            foreach (Gesture g in this.gestures)
            {
                //g.Reset();
            }
        }
    }
}

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using GestoMusic;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Samples.Kinect.WpfViewers;
using System.Diagnostics;
using System.ComponentModel;
using System;

namespace Fizbin.Kinect.Gestures.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly KinectSensorChooser sensorChooser = new KinectSensorChooser();

        private Skeleton[] skeletons = new Skeleton[0];

        private ObservableCollection<string> _logs;
        public ObservableCollection<string> Logs
        {
            get
            {
                if (_logs == null)
                {
                    _logs = new ObservableCollection<string>();
                }
                return _logs;
            }
        }


        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();

            // initialize the Kinect sensor manager
            KinectSensorManager = new KinectSensorManager();
            KinectSensorManager.KinectSensorChanged += this.KinectSensorChanged;

            // locate an available sensor
            sensorChooser.Start();

            // bind chooser's sensor value to the local sensor manager
            var kinectSensorBinding = new Binding("Kinect") { Source = this.sensorChooser };
            BindingOperations.SetBinding(this.KinectSensorManager, KinectSensorManager.KinectSensorProperty, kinectSensorBinding);
        }

        #region Kinect Discovery & Setup

        private void KinectSensorChanged(object sender, KinectSensorManagerEventArgs<KinectSensor> args)
        {
            if (null != args.OldValue)
                UninitializeKinectServices(args.OldValue);

            if (null != args.NewValue)
                InitializeKinectServices(KinectSensorManager, args.NewValue);
        }

        /// <summary>
        /// Kinect enabled apps should customize which Kinect services it initializes here.
        /// </summary>
        /// <param name="kinectSensorManager"></param>
        /// <param name="sensor"></param>
        private void InitializeKinectServices(KinectSensorManager kinectSensorManager, KinectSensor sensor)
        {
            // Application should enable all streams first.

            // configure the color stream
            kinectSensorManager.ColorFormat = ColorImageFormat.RgbResolution640x480Fps30;

            kinectSensorManager.TransformSmoothParameters =
                new TransformSmoothParameters
                {
                    Smoothing = 0.5f,
                    Correction = 0.5f,
                    Prediction = 0.5f,
                    JitterRadius = 0.05f,
                    MaxDeviationRadius = 0.04f
                };

            // configure the skeleton stream
            sensor.SkeletonFrameReady += OnSkeletonFrameReady;
            kinectSensorManager.SkeletonStreamEnabled = true;

            // initialize the gesture recognizer
            gesturesObserver = new GesturesObserver();
            var samplesFactory = new SamplesFactory();
            var gitare = samplesFactory.GetGitare();
            var plate = samplesFactory.GetGitare();
            var tube = samplesFactory.GetGitare();
            var drum = samplesFactory.GetGitare();
            gesturesObserver.TrackDiscretGesture(GestureType.HammerLeft, gitare);
            gesturesObserver.TrackDiscretGesture(GestureType.HammerRight, plate);
            gesturesObserver.TrackDiscretGesture(GestureType.StepLeft, tube);
            gesturesObserver.TrackDiscretGesture(GestureType.StepRight, drum);
            gesturesObserver.TrackDiscretGesture(GestureType.Head, drum);

            float pitchDetla = 0.6f;
            var s = new ContinuesGestureSettings
                {
                    ActivationGesture = GestureType.ZoomOut,
                    DeactivationGesture = GestureType.ZoomIn,

                };
            s.SkeletonAdjustment += (sender, args) =>
                {
                    var settings = (ContinuesGestureSettings)sender;

                    settings.Pitch = (args.Skeleton.Joints[JointType.WristLeft].Position.Y -
                                      args.Skeleton.Joints[JointType.WristRight].Position.Y) * pitchDetla;
                };
            gesturesObserver.TrackContinuesGesture(s, drum);

            gesturesObserver.GestureSamplePlayed += GestureSamplePlayed;

            kinectSensorManager.KinectSensorEnabled = true;

            if (!kinectSensorManager.KinectSensorAppConflict)
            {
                // addition configuration, as needed
            }
        }

        private void GestureSamplePlayed(object sender, GestureSampleArgs e)
        {
            var log = e.GestureEventArgs.GestureType.ToString();
            Logs.Add(log);
            Gesture = log;
        }

        /// <summary>
        /// Kinect enabled apps should uninitialize all Kinect services that were initialized in InitializeKinectServices() here.
        /// </summary>
        /// <param name="sensor"></param>
        private void UninitializeKinectServices(KinectSensor sensor)
        {

        }

        #endregion Kinect Discovery & Setup

        #region Properties

        public static readonly DependencyProperty KinectSensorManagerProperty =
            DependencyProperty.Register(
                "KinectSensorManager",
                typeof(KinectSensorManager),
                typeof(MainWindow),
                new PropertyMetadata(null));

        public KinectSensorManager KinectSensorManager
        {
            get { return (KinectSensorManager)GetValue(KinectSensorManagerProperty); }
            set { SetValue(KinectSensorManagerProperty, value); }
        }

        /// <summary>
        /// Gets or sets the last recognized gesture.
        /// </summary>
        private string _gesture;

        private GesturesObserver gesturesObserver;

        public String Gesture
        {
            get { return _gesture; }

            private set
            {
                if (_gesture == value)
                    return;

                _gesture = value;

                Debug.WriteLine("Gesture = " + _gesture);

                if (this.PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Gesture"));
            }
        }

        #endregion Properties

        #region Events

        /// <summary>
        /// Event implementing INotifyPropertyChanged interface.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Event Handlers


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame == null)
                    return;

                // resize the skeletons array if needed
                if (skeletons.Length != frame.SkeletonArrayLength)
                    skeletons = new Skeleton[frame.SkeletonArrayLength];

                // get the skeleton data
                frame.CopySkeletonDataTo(skeletons);

                foreach (var skeleton in skeletons)
                {
                    // skip the skeleton if it is not being tracked
                    if (skeleton.TrackingState != SkeletonTrackingState.Tracked)
                        continue;

                    //Gesture = Math.Abs(skeleton.Joints[JointType.WristRight].Position.Y - skeleton.Joints[JointType.WristLeft].Position.Y).ToString();

                    // update the gesture controller
                    gesturesObserver.UpdateAllGestures(skeleton);
                }
            }
        }

        #endregion Event Handlers

    }
}

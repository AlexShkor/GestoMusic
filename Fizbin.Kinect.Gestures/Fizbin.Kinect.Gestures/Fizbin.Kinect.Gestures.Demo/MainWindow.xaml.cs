﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                    Prediction = 0.8f,
                    JitterRadius = 0.05f,
                    MaxDeviationRadius = 0.04f
                };

            // configure the skeleton stream
            sensor.SkeletonFrameReady += OnSkeletonFrameReady;
            kinectSensorManager.SkeletonStreamEnabled = true;

            // initialize the gesture recognizer
            _samplesFactory = new SamplesFactory();
            _metro = _samplesFactory.GetMetronom();
             _metro.PlayNonStop();


            CreateObserver(_samplesFactory);

            kinectSensorManager.KinectSensorEnabled = true;

            if (!kinectSensorManager.KinectSensorAppConflict)
            {
                // addition configuration, as needed
            }
        }

        private GesturesObserver CreateObserver(SamplesFactory samplesFactory)
        {
            var gesturesObserver = new GesturesObserver();


            gesturesObserver.TrackDiscretGesture(GestureType.HammerLeft, samplesFactory.GetDrumHandLeft());
            gesturesObserver.TrackDiscretGesture(GestureType.HammerRight, samplesFactory.GetDrumHandRight());
            gesturesObserver.TrackDiscretGesture(GestureType.StepLeft, samplesFactory.GetDrumLegLeft());
            gesturesObserver.TrackDiscretGesture(GestureType.StepRight, samplesFactory.GetDrumLegRight());
            gesturesObserver.TrackDiscretGesture(GestureType.Head, samplesFactory.GetDrumHead());
            gesturesObserver.TrackDiscretGesture(GestureType.JoinedHands, samplesFactory.GetGuitarLoop());
            gesturesObserver.TrackDiscretGesture(GestureType.Menu, samplesFactory.GetPianoLoop());

            _settings = new HandUpContiniousGestureSettings();
            gesturesObserver.TrackContinuesGesture(_settings, samplesFactory.Wawe());

            gesturesObserver.GestureSamplePlayed += GestureSamplePlayed;
            return gesturesObserver;
        }

        private void GestureSamplePlayed(object sender, GestureSampleArgs e)
        {
            //var log = e.GestureEventArgs.GestureType.ToString();
           // Logs.Add(log);
            //Gesture = log;
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

        private Dictionary<int, GesturesObserver> _observers = new Dictionary<int, GesturesObserver>();
        private HandUpContiniousGestureSettings _settings;
        private Sample _metro;
        private SamplesFactory _samplesFactory;

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
                var toRemove = _observers.Keys.ToList();
                foreach (var skeleton in skeletons)
                {
                    // skip the skeleton if it is not being tracked
                    if (skeleton.TrackingState != SkeletonTrackingState.Tracked)
                        continue;


                    //Gesture = _settings.Pitch.ToString();
                    //Gesture = Math.Abs(skeleton.Joints[JointType.WristRight].Position.Y - skeleton.Joints[JointType.WristLeft].Position.Y).ToString();

                    // update the gesture controller
                    if (!_observers.ContainsKey(skeleton.TrackingId))
                    {
                        _observers[skeleton.TrackingId] = CreateObserver(_samplesFactory);
                    }
                    _observers[skeleton.TrackingId].UpdateAllGestures(skeleton);
                    toRemove.Remove(skeleton.TrackingId);
                }
                foreach (var i in toRemove)
                {
                    _observers.Remove(i);
                }
            }
        }

        #endregion Event Handlers

    }
}

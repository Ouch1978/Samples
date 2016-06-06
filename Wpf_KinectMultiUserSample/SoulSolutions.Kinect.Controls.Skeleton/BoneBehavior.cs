using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace SoulSolutions.Kinect.Controls.Skeleton
{
    /// <summary>
    /// Behavior that stretches its associated element between the two frameworkelements specified
    /// Class provided by András Velvárt (@vbandi) great work with Kinect (unmodified).
    /// </summary>
    public class BoneBehavior : Behavior<FrameworkElement>
    {
        private bool _isAttached;
        private FrameworkElement _startJoint;
        private FrameworkElement _endJoint;
        private RotateTransform _rotateTransform;
        private ScaleTransform _scaleTransform;
        private Canvas _parent;

        /// <summary>
        /// Gets or sets the stretch direction.
        /// </summary>
        public BoneStretchDirections StretchDirection
        {
            get { return (BoneStretchDirections)GetValue(StretchDirectionProperty); }
            set { SetValue(StretchDirectionProperty, value); }
        }
        ///<summary>
        /// Identifies the <see cref="StretchDirection" /> dependency property.
        ///</summary>
        public static readonly DependencyProperty StretchDirectionProperty =
            DependencyProperty.Register("StretchDirection", typeof(BoneStretchDirections), typeof(BoneBehavior), new UIPropertyMetadata(BoneStretchDirections.Horizontal, DependencyPropertyChanged));

        /// <summary>
        /// Gets or sets the name of the starting element.
        /// </summary>
        [CustomPropertyValueEditor(CustomPropertyValueEditor.Element)]
        public string StartJointElementName
        {
            get { return (string)GetValue(StartJointElementNameProperty); }
            set { SetValue(StartJointElementNameProperty, value); }
        }
        ///<summary>
        /// Identifies the <see cref="StartJointElementName" /> dependency property.
        ///</summary>
        public static readonly DependencyProperty StartJointElementNameProperty =
            DependencyProperty.Register("StartJointElementName", typeof(string), typeof(BoneBehavior), new UIPropertyMetadata("", DependencyPropertyChanged));

        /// <summary>
        /// Gets or sets the name of the ending element.
        /// </summary>
        [CustomPropertyValueEditor(CustomPropertyValueEditor.Element)]
        public string EndJointElementName
        {
            get { return (string)GetValue(EndJointElementNameProperty); }
            set { SetValue(EndJointElementNameProperty, value); }
        }
        ///<summary>
        /// Identifies the <see cref="EndJointElementName" /> dependency property.
        ///</summary>
        public static readonly DependencyProperty EndJointElementNameProperty =
            DependencyProperty.Register("EndJointElementName", typeof(string), typeof(BoneBehavior), new UIPropertyMetadata("", DependencyPropertyChanged));

        private static void DependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var behavior = ((BoneBehavior)sender);
            if (args.NewValue == args.OldValue || !behavior._isAttached) return;
            behavior.Cleanup();
            behavior.Initialize();
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectLoaded;
            _isAttached = true;
        }

        void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
            Initialize();
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            Cleanup();
            _isAttached = false;
            base.OnDetaching();
        }

        private void Initialize()
        {
            _parent = VisualTreeHelper.GetParent(AssociatedObject) as Canvas;
            if (_parent == null)
                return;

            foreach (var child in _parent.Children)
            {
                var childElement = child as FrameworkElement;
                if (childElement == null)
                    continue;
                var name = childElement.GetValue(FrameworkElement.NameProperty) as string;
                if (name == null)
                    continue;
                if (name == StartJointElementName)
                {
                    _startJoint = childElement;
                }
                else if (name == EndJointElementName)
                {
                    _endJoint = childElement;
                }
            }

            if (_startJoint == null || _endJoint == null)
            {
                Cleanup();
                return;
            }

            _startJoint.LayoutUpdated += JointLayoutUpdated;
            _endJoint.LayoutUpdated += JointLayoutUpdated;

            PrepareBone();
            JointLayoutUpdated(null, null);
        }

        private void Cleanup()
        {
            AssociatedObject.Visibility = Visibility.Collapsed;
            if (_startJoint != null)
                _startJoint.LayoutUpdated -= JointLayoutUpdated;
            if (_endJoint != null)
                _endJoint.LayoutUpdated -= JointLayoutUpdated;
            _parent = null;
            _startJoint = null;
            _endJoint = null;
        }

        private void PrepareBone()
        {
            AssociatedObject.Visibility = Visibility.Visible;
            AssociatedObject.RenderTransformOrigin = new Point(
                StretchDirection == BoneStretchDirections.Horizontal ? 0 : 0.5,
                StretchDirection == BoneStretchDirections.Horizontal ? 0.5 : 0
                );
            _rotateTransform = new RotateTransform();
            _scaleTransform = new ScaleTransform();
            AssociatedObject.RenderTransform = _rotateTransform;
            AssociatedObject.LayoutTransform = _scaleTransform;
        }

        void JointLayoutUpdated(object sender, EventArgs e)
        {
            if (_startJoint == null || _endJoint == null)
            {
                Cleanup();
                return;
            }
            var boneOffset = new Point(
                StretchDirection == BoneStretchDirections.Horizontal ? 0 : -AssociatedObject.ActualWidth / 2,
                StretchDirection == BoneStretchDirections.Horizontal ? -AssociatedObject.ActualHeight / 2 : 0
            );
            var startCenterPoint = CalculateCenter(_startJoint);
            var endCenterPoint = CalculateCenter(_endJoint);
            Canvas.SetTop(AssociatedObject, startCenterPoint.Y + boneOffset.Y);
            Canvas.SetLeft(AssociatedObject, startCenterPoint.X + boneOffset.X);

            var length = Math.Sqrt(Math.Pow(startCenterPoint.X - endCenterPoint.X, 2) + Math.Pow(startCenterPoint.Y - endCenterPoint.Y, 2));
            if (StretchDirection == BoneStretchDirections.Horizontal)
            {
                if (AssociatedObject.ActualWidth > 0) _scaleTransform.ScaleX = length / AssociatedObject.ActualWidth;
            }
            else
            {
                if (AssociatedObject.ActualHeight > 0) _scaleTransform.ScaleY = length / AssociatedObject.ActualHeight;
            }
            var angle = CalculateAngle(startCenterPoint, endCenterPoint);
            _rotateTransform.Angle = angle;
        }

        private double CalculateAngle(Point point1, Point point2)
        {
            var angleMod = 0;
            if (point1.Y >= point2.Y)
                angleMod += 180;
            if (StretchDirection == BoneStretchDirections.Horizontal)
                angleMod += 90;
            return -Math.Atan((point1.X - point2.X) / (point1.Y - point2.Y)) * 180 / Math.PI + angleMod;
        }

        private Point CalculateCenter(FrameworkElement element)
        {
            var newPoint = element.TransformToAncestor(_parent).Transform(new Point(element.ActualWidth / 2, element.ActualHeight / 2));
            return newPoint;
        }
    }

    /// <summary>
    /// Enumerates the various stretch directions for the BoneBehavior
    /// </summary>
    public enum BoneStretchDirections
    {
        /// <summary>
        /// Stretching should be done along the horizontal edge of the control
        /// </summary>
        Horizontal,
        /// <summary>
        /// Stretching should be done along the vertical edge of the control
        /// </summary>
        Vertical
    }
}

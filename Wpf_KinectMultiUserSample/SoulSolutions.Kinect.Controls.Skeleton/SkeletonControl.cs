//This code is provided as is and with no warrentee. Use at your own risk, license is MS-PL, feel free to modifiy and use in your projects as needed.

#region

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Coding4Fun.Kinect.Wpf;
using Microsoft.Research.Kinect.Nui;

#endregion

namespace SoulSolutions.Kinect.Controls.Skeleton
{
    /// <summary>
    /// Kinect Skeleton for you to customise and simply drop into your projects and style in Blend.
    /// </summary>
    [TemplatePart(Name = PartHead, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartHandLeft, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartHandRight, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartHipCenter, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartFootRight, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartFootLeft, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartAnkleLeft, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartAnkleRight, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartElbowLeft, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartElbowRight, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartHipLeft, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartHipRight, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartKneeLeft, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartKneeRight, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartShoulderCenter, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartShoulderLeft, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartShoulderRight, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartSpine, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartWristLeft, Type = typeof (FrameworkElement))]
    [TemplatePart(Name = PartWristRight, Type = typeof (FrameworkElement))]
    public class SkeletonControl : Control
    {
        private const string PartHead = "PART_Head";
        private const string PartHandLeft = "PART_HandLeft";
        private const string PartHandRight = "PART_HandRight";
        private const string PartHipCenter = "PART_HipCenter";
        private const string PartFootRight = "PART_FootRight";
        private const string PartFootLeft = "PART_FootLeft";
        private const string PartAnkleLeft = "PART_AnkleLeft";
        private const string PartAnkleRight = "PART_AnkleRight";
        private const string PartElbowLeft = "PART_ElbowLeft";
        private const string PartElbowRight = "PART_ElbowRight";
        private const string PartHipLeft = "PART_HipLeft";
        private const string PartHipRight = "PART_HipRight";
        private const string PartKneeLeft = "PART_KneeLeft";
        private const string PartKneeRight = "PART_KneeRight";
        private const string PartShoulderCenter = "PART_ShoulderCenter";
        private const string PartShoulderLeft = "PART_ShoulderLeft";
        private const string PartShoulderRight = "PART_ShoulderRight";
        private const string PartSpine = "PART_Spine";
        private const string PartWristLeft = "PART_WristLeft";
        private const string PartWristRight = "PART_WristRight";
        public static readonly DependencyProperty BoneFillProperty = DependencyProperty.Register("BoneFill", typeof (Brush), typeof (SkeletonControl), null);

        private FrameworkElement ankleLeftUi;
        private FrameworkElement ankleRightUi;
        private FrameworkElement elbowLeftUi;
        private FrameworkElement elbowRightUi;
        private FrameworkElement footLeftUi;
        private FrameworkElement footRightUi;
        private FrameworkElement handLeftUi;
        private FrameworkElement handRightUi;
        private FrameworkElement headUi;
        private FrameworkElement hipCenterUi;
        private FrameworkElement hipLeftUi;
        private FrameworkElement hipRightUi;
        private FrameworkElement kneeLeftUi;
        private FrameworkElement kneeRightUi;
        private FrameworkElement shoulderCenterUi;
        private FrameworkElement shoulderLeftUi;
        private FrameworkElement shoulderRightUi;
        private SkeletonData skeletonData;
        private FrameworkElement spineUi;
        private FrameworkElement wristLeftUi;
        private FrameworkElement wristRightUi;

        static SkeletonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (SkeletonControl), new FrameworkPropertyMetadata(typeof (SkeletonControl)));
        }

        public Brush BoneFill
        {
            get { return (Brush) GetValue(BoneFillProperty); }
            set { SetValue(BoneFillProperty, value); }
        }

        public SkeletonData SkeletonData
        {
            get { return skeletonData; }
            set
            {
                skeletonData = value;
                SetUiPosition(headUi, skeletonData.Joints[JointID.Head]);
                SetUiPosition(handLeftUi, skeletonData.Joints[JointID.HandLeft]);
                SetUiPosition(handRightUi, skeletonData.Joints[JointID.HandRight]);
                SetUiPosition(hipCenterUi, skeletonData.Joints[JointID.HipCenter]);
                SetUiPosition(footRightUi, skeletonData.Joints[JointID.FootRight]);
                SetUiPosition(footLeftUi, skeletonData.Joints[JointID.FootLeft]);
                SetUiPosition(ankleLeftUi, skeletonData.Joints[JointID.AnkleLeft]);
                SetUiPosition(ankleRightUi, skeletonData.Joints[JointID.AnkleRight]);
                SetUiPosition(elbowLeftUi, skeletonData.Joints[JointID.ElbowLeft]);
                SetUiPosition(elbowRightUi, skeletonData.Joints[JointID.ElbowRight]);
                SetUiPosition(hipLeftUi, skeletonData.Joints[JointID.HipLeft]);
                SetUiPosition(hipRightUi, skeletonData.Joints[JointID.HipRight]);
                SetUiPosition(kneeLeftUi, skeletonData.Joints[JointID.KneeLeft]);
                SetUiPosition(kneeRightUi, skeletonData.Joints[JointID.KneeRight]);
                SetUiPosition(shoulderCenterUi, skeletonData.Joints[JointID.ShoulderCenter]);
                SetUiPosition(shoulderLeftUi, skeletonData.Joints[JointID.ShoulderLeft]);
                SetUiPosition(shoulderRightUi, skeletonData.Joints[JointID.ShoulderRight]);
                SetUiPosition(spineUi, skeletonData.Joints[JointID.Spine]);
                SetUiPosition(wristLeftUi, skeletonData.Joints[JointID.WristLeft]);
                SetUiPosition(wristRightUi, skeletonData.Joints[JointID.WristRight]);
            }
        }


        //*EXPERIMENTAL*//
        //Poses to build on need more models to verify correct, works well for 6foot4 Male :)
        //Smaller children have already proved these values need to be changed to be proportional rather then absolute.

        public bool Jump
        {
            get { return SkeletonData.Joints[JointID.FootLeft].Position.Y > -0.7 && SkeletonData.Joints[JointID.FootRight].Position.Y > -0.7; }
        }

        public bool LeftArmOut
        {
            get { return ((SkeletonData.Joints[JointID.HandLeft].Position.X - SkeletonData.Joints[JointID.ShoulderLeft].Position.X) < -0.5); }
        }

        public bool RightArmOut
        {
            get { return ((SkeletonData.Joints[JointID.HandRight].Position.X - SkeletonData.Joints[JointID.ShoulderRight].Position.X) > 0.5); }
        }

        public bool RightArmUp
        {
            get { return ((SkeletonData.Joints[JointID.HandRight].Position.Y - SkeletonData.Joints[JointID.Head].Position.Y) > 0); }
        }

        public bool LeftArmUp
        {
            get { return ((SkeletonData.Joints[JointID.HandLeft].Position.Y - SkeletonData.Joints[JointID.Head].Position.Y) > 0); }
        }

        public bool Crouched
        {
            get { return (Math.Abs(SkeletonData.Joints[JointID.FootLeft].Position.Y - SkeletonData.Joints[JointID.HandLeft].Position.Y) < 0.2); }
        }

        public bool HandsTogether
        {
            get
            {
                return (Math.Abs(SkeletonData.Joints[JointID.HandRight].Position.Y - SkeletonData.Joints[JointID.HandLeft].Position.Y) +
                        Math.Abs(SkeletonData.Joints[JointID.HandRight].Position.X - SkeletonData.Joints[JointID.HandLeft].Position.X) < 0.3);
            }
        }

        public override void OnApplyTemplate()
        {
            headUi = GetTemplateChild(PartHead) as FrameworkElement;
            handLeftUi = GetTemplateChild(PartHandLeft) as FrameworkElement;
            handRightUi = GetTemplateChild(PartHandRight) as FrameworkElement;
            hipCenterUi = GetTemplateChild(PartHipCenter) as FrameworkElement;
            footRightUi = GetTemplateChild(PartFootRight) as FrameworkElement;
            footLeftUi = GetTemplateChild(PartFootLeft) as FrameworkElement;
            ankleLeftUi = GetTemplateChild(PartAnkleLeft) as FrameworkElement;
            ankleRightUi = GetTemplateChild(PartAnkleRight) as FrameworkElement;
            elbowLeftUi = GetTemplateChild(PartElbowLeft) as FrameworkElement;
            elbowRightUi = GetTemplateChild(PartElbowRight) as FrameworkElement;
            hipLeftUi = GetTemplateChild(PartHipLeft) as FrameworkElement;
            hipRightUi = GetTemplateChild(PartHipRight) as FrameworkElement;
            kneeLeftUi = GetTemplateChild(PartKneeLeft) as FrameworkElement;
            kneeRightUi = GetTemplateChild(PartKneeRight) as FrameworkElement;
            shoulderCenterUi = GetTemplateChild(PartShoulderCenter) as FrameworkElement;
            shoulderLeftUi = GetTemplateChild(PartShoulderLeft) as FrameworkElement;
            shoulderRightUi = GetTemplateChild(PartShoulderRight) as FrameworkElement;
            spineUi = GetTemplateChild(PartSpine) as FrameworkElement;
            wristLeftUi = GetTemplateChild(PartWristLeft) as FrameworkElement;
            wristRightUi = GetTemplateChild(PartWristRight) as FrameworkElement;
            base.OnApplyTemplate();
        }

        private void SetUiPosition(FrameworkElement ui, Joint joint)
        {
            if (ui != null && ActualWidth > 0 && ActualHeight > 0)
            {
                var scaledJoint = joint.ScaleTo((int) ActualWidth, (int) ActualHeight, 1f, 1f);
                Canvas.SetLeft(ui, scaledJoint.Position.X - ui.ActualWidth/2);
                Canvas.SetTop(ui, scaledJoint.Position.Y - ui.ActualHeight/2);
            }
        }
    }
}
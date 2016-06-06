using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Research.Kinect.Nui;

namespace Wpf_KinectSample03
{
    public partial class MainWindow : Window
    {
        //Instantiate the Kinect runtime. Required to initialize the device.
        //IMPORTANT NOTE: You can pass the device ID here, in case more than one Kinect device is connected.
        Runtime runtime = new Runtime();

        public MainWindow()
        {
            InitializeComponent();

            //Runtime initialization is handled when the window is opened. When the window
            //is closed, the runtime MUST be unitialized.
            this.Loaded += new RoutedEventHandler( MainWindow_Loaded );
            this.Unloaded += new RoutedEventHandler( MainWindow_Unloaded );

            //Handle the content obtained from the video camera, once received.
            runtime.VideoFrameReady += new EventHandler<Microsoft.Research.Kinect.Nui.ImageFrameReadyEventArgs>( runtime_VideoFrameReady );

            runtime.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>( runtime_SkeletonFrameReady );
        }

        void runtime_SkeletonFrameReady( object sender , SkeletonFrameReadyEventArgs e )
        {
            SkeletonFrame skeletonSet = e.SkeletonFrame;

            SkeletonData data = ( from s in skeletonSet.Skeletons
                                  where s.TrackingState == SkeletonTrackingState.Tracked
                                  select s ).FirstOrDefault();

            //這段是我自己加上來的，可以避免當在拍攝的範圍中偵測不到人的時候會發生Exception的情況            
            if( data == null )
            {
                return;
            }

            //下面這三行就是直接透過Kinect SDK提供的Enum去抓取頭、左手和右手的位置，再透過SetFrameworkElementPosition方法將抓到的位置套到畫面上預先放好的控制項中
            SetFrameworkElementPosition( grdHead , data.Joints[ JointID.Head ] );
            SetFrameworkElementPosition( grdLeftHand , data.Joints[ JointID.HandLeft ] );
            SetFrameworkElementPosition( grdRightHand , data.Joints[ JointID.HandRight ] );
        }

        //這個方法會傳入代表某個點的控制項，還有該點相對於Kinect擷取到的座標
        private void SetFrameworkElementPosition( FrameworkElement frameworkElement , Joint joint )
        {

            Microsoft.Research.Kinect.Nui.Vector vector = new Microsoft.Research.Kinect.Nui.Vector();
            //下面的兩行為透過呼叫ScaleVector方法，將Kinect的座標系換算回螢幕座標系
            vector.X = ScaleVector( 640 , joint.Position.X );
            //別忘了，Y座標軸往上是負的，所以這邊要變負數才行
            vector.Y = ScaleVector( 480 , -joint.Position.Y  );
            vector.Z = joint.Position.Z;

            //宣告一個新的Joint物件來存放重新計算過的座標
            Joint updatedJoint = new Joint();
            updatedJoint.ID = joint.ID;
            updatedJoint.TrackingState = JointTrackingState.Tracked;
            updatedJoint.Position = vector;

            //重新設定控制項的座標位置，後面我自己加上了加回控制項大小的程式碼，以減少誤差
            Canvas.SetLeft( frameworkElement , updatedJoint.Position.X + frameworkElement.ActualWidth  );
            Canvas.SetTop( frameworkElement , updatedJoint.Position.Y + frameworkElement.ActualHeight );
        }

        //這個方法會將Kinect的座標系換算回螢幕的座標系
        private float ScaleVector( int length , float position )
        {
            float value = ( ( ( ( ( float ) length ) / 1f ) / 2f ) * position ) + ( length / 2 );
            if( value > length )
            {
                return ( float ) length;
            }
            if( value < 0f )
            {
                return 0f;
            }
            return value;
        }

        void MainWindow_Unloaded( object sender , RoutedEventArgs e )
        {
            runtime.Uninitialize();
        }

        void MainWindow_Loaded( object sender , RoutedEventArgs e )
        {
            try
            {
                runtime.Initialize( Microsoft.Research.Kinect.Nui.RuntimeOptions.UseColor | RuntimeOptions.UseSkeletalTracking );

                runtime.VideoStream.Open( ImageStreamType.Video , 2 , ImageResolution.Resolution640x480 , ImageType.Color );
            }
            catch( Exception )
            {
                MessageBox.Show( "Kinect裝置初始化失敗!!" );
            }
        }
        void runtime_VideoFrameReady( object sender , Microsoft.Research.Kinect.Nui.ImageFrameReadyEventArgs e )
        {
            PlanarImage image = e.ImageFrame.Image;

            BitmapSource source = BitmapSource.Create( image.Width , image.Height , 96 , 96 ,
                PixelFormats.Bgr32 , null , image.Bits , image.Width * image.BytesPerPixel );
            videoImage.Source = source;
        }
    }
}

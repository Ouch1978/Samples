using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Research.Kinect.Nui;

namespace Wpf_MultiUserSample
{
    public partial class MainWindow : Window
    {
        Runtime runtime = new Runtime();

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler( MainWindow_Loaded );
            this.Unloaded += new RoutedEventHandler( MainWindow_Unloaded );

            runtime.VideoFrameReady += new EventHandler<Microsoft.Research.Kinect.Nui.ImageFrameReadyEventArgs>( runtime_VideoFrameReady );

            runtime.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>( runtime_SkeletonFrameReady );
        }

        void runtime_SkeletonFrameReady( object sender , SkeletonFrameReadyEventArgs e )
        {
            SkeletonFrame skeletonSet = e.SkeletonFrame;

            //取得Kinect捕捉到的玩家數量
            int playersCount = ( from s in skeletonSet.Skeletons
                                 where s.TrackingState == SkeletonTrackingState.Tracked
                                 select s ).Count();

            skeletonPlayer1.SkeletonData = ( from s in skeletonSet.Skeletons
                                             where s.TrackingState == SkeletonTrackingState.Tracked
                                             select s ).FirstOrDefault();

            //如果捕捉到的玩家數是兩位，則顯示第二位玩家的骨架
            if( playersCount == 2 )
            {
                //透過LINQ的ElementAt方法取得指定的元素(第二位玩家的SkeletonData)
                skeletonPlayer2.SkeletonData = ( from s in skeletonSet.Skeletons
                                                 where s.TrackingState == SkeletonTrackingState.Tracked
                                                 select s ).ElementAt( 1 );
            }
        }


        void MainWindow_Unloaded( object sender , RoutedEventArgs e )
        {
            runtime.Uninitialize();
        }

        void MainWindow_Loaded( object sender , RoutedEventArgs e )
        {
            //Since only a color video stream is needed, RuntimeOptions.UseColor is used.
            runtime.Initialize( Microsoft.Research.Kinect.Nui.RuntimeOptions.UseColor | RuntimeOptions.UseSkeletalTracking );

            //You can adjust the resolution here.
            runtime.VideoStream.Open( ImageStreamType.Video , 2 , ImageResolution.Resolution640x480 , ImageType.Color );
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

using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Research.Kinect.Nui;

namespace Wpf_KinectSample
{
    public partial class MainWindow : Window
    {
        private Runtime _runtime;

        public MainWindow()
        {
            InitializeComponent();

            InitializeRuntime();
        }

        private void InitializeRuntime()
        {
            UninitializeRuntime();

            _runtime = new Runtime();

            //因為我們只要取得基本的鏡頭拍到的畫面而已，所以只需要將RuntimeOptions設定為UseColor即可
            _runtime.Initialize( RuntimeOptions.UseColor );

            //透過VideoStream.Open方法開始取得視訊串流
            _runtime.VideoStream.Open( ImageStreamType.Video , 2 , ImageResolution.Resolution640x480 , ImageType.Color );

            //替VideoFrameReady加入EventHandler，把畫面畫在Image中。
            _runtime.VideoFrameReady += new EventHandler<ImageFrameReadyEventArgs>( RuntimeVideoFrameReady );
        }

        private void UninitializeRuntime()
        {
            if( _runtime == null )
            {
                return;
            }

            _runtime.VideoFrameReady -= RuntimeVideoFrameReady;

            _runtime.Uninitialize();

            _runtime = null;
        }

        private void RuntimeVideoFrameReady( object sender , ImageFrameReadyEventArgs e )
        {
            PlanarImage image = e.ImageFrame.Image;

            byte[] pixels = image.Bits;

            //將取得的圖片轉為BitmapSource，並用來當作Image控制項的Source
            imgVideoFrame.Source = BitmapSource.Create( image.Width , image.Height , 96 , 96 , PixelFormats.Bgr32 , null , pixels , image.Width * PixelFormats.Bgr32.BitsPerPixel / 8 );
        }

        private void btnStart_Click( object sender , RoutedEventArgs e )
        {
            InitializeRuntime();
        }

        private void btnStop_Click( object sender , RoutedEventArgs e )
        {
            UninitializeRuntime();
        }

        private void btnSavePicture_Click( object sender , RoutedEventArgs e )
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            encoder.Frames.Add( BitmapFrame.Create( BitmapFrame.Create( imgVideoFrame.Source as BitmapSource ) ) );

            Microsoft.Win32.SaveFileDialog openFileDialog = new Microsoft.Win32.SaveFileDialog();
            openFileDialog.FileName = "MyImage"; 
            openFileDialog.DefaultExt = ".jpg"; 
            openFileDialog.Filter = "Jpeg Image (.jpg)|*.jpg"; 

            Nullable<bool> result = openFileDialog.ShowDialog();

            string fileName = string.Empty;

            if( result == true )
            {
                fileName = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            using( var stream = new FileStream( fileName , FileMode.Create ) )
            {
                encoder.Save( stream );
            }
        }
    }
}

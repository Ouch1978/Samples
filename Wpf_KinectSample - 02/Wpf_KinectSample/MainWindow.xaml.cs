using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Research.Kinect.Nui;

using Coding4Fun.Kinect.Wpf;

//在專案中加入Kinect.Toolkit命名空間的引用
using Kinect.Toolkit;

namespace Wpf_KinectSample
{
    public partial class MainWindow : Window
    {
        private Runtime _runtime;

        //在Kinect.Toolkit中有一個ColorStreamManager可以幫助我們取得一般的視訊畫面
        private ColorStreamManager _colorStreamManager;

        public MainWindow()
        {
            InitializeComponent();

            InitializeRuntime();
        }

        private void InitializeRuntime()
        {
            UninitializeRuntime();

            _runtime = new Runtime();

            //在建立Runtime 的Instance的同時，也順便建立ColorStreamManager的Instance
            _colorStreamManager = new ColorStreamManager();

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

            _colorStreamManager = null;

            _runtime = null;
        }

        private void RuntimeVideoFrameReady( object sender , ImageFrameReadyEventArgs e )
        {
            //透過ColorStreamManager.Update()方法，直接傳入ImageFrameReadyEventArgs，取得BitmapSource
            imgVideoFrame.Source = _colorStreamManager.Update( e );

            
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

            //要儲存圖片的話，就可以搭配Coding4Func提供的Save()擴充方法，會比較方便喔!!
            ( _colorStreamManager.ColorBitmap as BitmapSource ).Save( fileName , ImageFormat.Jpeg );
        }
    }
}

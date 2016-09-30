using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Html2Markdown;
using HtmlAgilityPack;

namespace HtmlToMdExporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int WM_SYSCOMMAND = 0x112;
        private HwndSource hwndSource;
        IntPtr retInt = IntPtr.Zero;


        private HttpClient _httpClient;

        public int CurrentPage { get; set; } = 1;

        public string BaseUrl { get; set; } = "https://dotblogs.com.tw";

        public string UserName { get; set; } = "Ouch1978";

        public string PostLinkXPath { get; set; } = "/html/body/div[1]/div/div/div[1]/article/header/h3/a";

        public string PostBodyXPath { get; set; } = "/html/body/div[@class='top-wrapper']/div/div/div[1]/article/div[1]";

        public string Error404TagXPath { get; set; } = "/html/body/div[1]/div/div/div[1]/article/header/ul/li";

        public string OutputPath { get; set; } = @"C:\Temp\";

        public MainWindow()
        {
            InitializeComponent();
            this.SourceInitialized += new System.EventHandler( MainWindow_SourceInitialized );

        }

        void MainWindow_SourceInitialized( object sender, System.EventArgs e )
        {
            hwndSource = PresentationSource.FromVisual( (Visual) sender ) as HwndSource;
            hwndSource.AddHook( new HwndSourceHook( WndProc ) );
        }


        private IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
        {
            Debug.WriteLine( "WndProc messages: " + msg.ToString() );

            if( msg == WM_SYSCOMMAND )
            {
                Debug.WriteLine( "WndProc messages: " + msg.ToString() );
            }

            return IntPtr.Zero;
        }

        public enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        [DllImport( "user32.dll", CharSet = CharSet.Auto )]
        private static extern IntPtr SendMessage( IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam );

        private void ResizeWindow( ResizeDirection direction )
        {
            SendMessage( hwndSource.Handle, WM_SYSCOMMAND, (IntPtr) ( 61440 + direction ), IntPtr.Zero );
        }

        private void ResetCursor( object sender, MouseEventArgs e )
        {
            if( Mouse.LeftButton != MouseButtonState.Pressed )
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void bdrWindowTitle_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            this.DragMove();
        }

        private void btnClose_Click( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void btnMaximize_Click( object sender, RoutedEventArgs e )
        {
            this.WindowState = ( this.WindowState != WindowState.Maximized ) ? WindowState.Maximized : WindowState.Normal;
        }

        private void btnMinimize_Click( object sender, RoutedEventArgs e )
        {
            this.WindowState = WindowState.Minimized;
        }

        private void thumb_PreviewMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            Thumb thumb = sender as Thumb;

            switch( thumb.Name.Substring( 5 ) )
            {
                case "Top":
                    ResizeWindow( ResizeDirection.Top );
                    break;
                case "Bottom":
                    ResizeWindow( ResizeDirection.Bottom );
                    break;
                case "Left":
                    ResizeWindow( ResizeDirection.Left );
                    break;
                case "Right":
                    ResizeWindow( ResizeDirection.Right );
                    break;
                case "TopLeft":
                    ResizeWindow( ResizeDirection.TopLeft );
                    break;
                case "TopRight":
                    ResizeWindow( ResizeDirection.TopRight );
                    break;
                case "BottomLeft":
                    ResizeWindow( ResizeDirection.BottomLeft );
                    break;
                case "BottomRight":
                    ResizeWindow( ResizeDirection.BottomRight );
                    break;
                default:
                    break;
            }

        }

        private async void btnGo_Click( object sender, RoutedEventArgs e )
        {
            this.btnGo.IsEnabled = false;
            _httpClient = new HttpClient();

            var response = await _httpClient.GetAsync( $"{BaseUrl}/{UserName}/{CurrentPage}" );

            response.EnsureSuccessStatusCode();

            var task = response.Content.ReadAsStringAsync();

            do
            {
                var htmlDocument = new HtmlDocument();

                htmlDocument.LoadHtml( task.Result );

                if( htmlDocument.DocumentNode.SelectSingleNode( Error404TagXPath ) != null &&
    htmlDocument.DocumentNode.SelectSingleNode( Error404TagXPath ).InnerText.Equals( "404" ) )
                {
                    break;
                }


                var postLinks = htmlDocument.DocumentNode.SelectNodes( PostLinkXPath );

                foreach( var postLink in postLinks )
                {
                    var postUrl = postLink.Attributes[ "href" ].Value;

                    var postName = postLink.InnerHtml;

                    SavePostAsMdFile( postUrl, postName );
                }

                txtCurrentPage.Text = ( ++CurrentPage ).ToString();

                response = await _httpClient.GetAsync( $"{BaseUrl}/{UserName}/{CurrentPage}" );

                response.EnsureSuccessStatusCode();

                task = response.Content.ReadAsStringAsync();

            } while( task.Exception == null );


            MessageBox.Show( "Done" );

            this.btnGo.IsEnabled = true;
        }

        private async void SavePostAsMdFile( string postUrl, string postName )
        {
            try
            {
                HttpClient httpClient = new HttpClient();

                string[] tempArray = postUrl.Split( '/' );

                string fileName = $"{tempArray[ 2 ]}-{tempArray[ 3 ]}-{tempArray[ 4 ]}-{tempArray[ 5 ]}.md";

                var response = await httpClient.GetAsync( $"{BaseUrl}{postUrl}" );

                response.EnsureSuccessStatusCode();

                var task = response.Content.ReadAsStringAsync();

                if( task.Exception == null )
                {

                    var htmlDocument = new HtmlDocument();

                    htmlDocument.LoadHtml( task.Result );

                    var postBody = htmlDocument.DocumentNode.SelectSingleNode( PostBodyXPath );

                    var html = postBody.InnerHtml;

                    var converter = new Converter();

                    var markdown = converter.Convert( html );

                    System.IO.File.WriteAllText( $"{OutputPath}{fileName}", markdown );

                }
            }
            catch( Exception ex )
            {
                // ignored
            }
        }
    }
}

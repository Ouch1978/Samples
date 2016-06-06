using System;
using System.Linq;
using System.Windows;

namespace Wpf_ConsoleApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );

            if( e.Args.Count() > 0 )
            {
                //呼叫出Console視窗
                ConsoleHelper.Show();

                //設定Console視窗的大小，注意，這邊的寬和高指的是文字的行數和列數，而不是畫面上的點喔!!
                Console.SetWindowSize( 80 , 24 );

                //設定Console視窗的起始位置
                Console.SetWindowPosition( 0 , 0 );

                //設定Console視窗的標題
                Console.Title = "WPF Console Demo";

                //設定文字前景色
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine( "Console視窗啟動" );

                //可以再次設定前景色，之後的文字就會以新的顏色列印
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine( "歡迎使用Console視窗!!" );

                Console.WriteLine( string.Format( "參數數目:{0}" , e.Args.Count().ToString() ) );

                string input = string.Empty;

                while( input.ToLower() != "exit" )
                {
                    Console.WriteLine( "請輸入exit關閉程式:" );

                    input = Console.ReadLine();
                }

                App.Current.Shutdown();
            }
            else
            {
                App.Current.StartupUri = new System.Uri( "MainWindow.xaml" , System.UriKind.RelativeOrAbsolute );
            }
        }
    }
}

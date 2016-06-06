using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Windows;

namespace SL_ReadConfigFile
{
    public partial class App : Application
    {
        private IDictionary<string , string> _configurations;
        public IDictionary<string , string> Configurations
        {
            get
            {
                return _configurations;
            }
        }

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup( object sender , StartupEventArgs e )
        {
            //這行要改成透過新增的Method來取得initParams，並且依照是否為OOB模式做不同的處理
            _configurations = LoadInitParams( e );

            this.RootVisual = new MainPage();
        }

        //這個Method是新增的部份，用來取代掉原來的Web.config取值方式。
        private static IDictionary<string , string> LoadInitParams( StartupEventArgs e )
        {
            IDictionary<string , string> initParams;

            //如果在OOB的環境下執行的話，則存取IsolatedStorage中的內容，否則的話則把資料寫入IsolatedStorage
            if( Application.Current.IsRunningOutOfBrowser )
            {
                using( IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication() )
                {
                    using( IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream( "InitParams.txt" , System.IO.FileMode.Open , isolatedStorageFile ) )
                    {
                        DataContractSerializer dataContractSerializer = new DataContractSerializer( typeof( Dictionary<string , string> ) );
                        initParams = ( Dictionary<string , string> ) dataContractSerializer.ReadObject( isolatedStorageFileStream );
                    }
                }
            }
            else
            {
                initParams = e.InitParams;

                using( IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication() )
                {
                    using( IsolatedStorageFileStream isolatedStorageFileStream = new IsolatedStorageFileStream( "InitParams.txt" , System.IO.FileMode.Create , isolatedStorageFile ) )
                    {
                        DataContractSerializer dataContractSerializer = new DataContractSerializer( typeof( Dictionary<string , string> ) );
                        dataContractSerializer.WriteObject( isolatedStorageFileStream , initParams );
                    }
                }
            }
            return initParams;
        }


        private void Application_Exit( object sender , EventArgs e )
        {

        }

        private void Application_UnhandledException( object sender , ApplicationUnhandledExceptionEventArgs e )
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if( !System.Diagnostics.Debugger.IsAttached )
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke( delegate { ReportErrorToDOM( e ); } );
            }
        }

        private void ReportErrorToDOM( ApplicationUnhandledExceptionEventArgs e )
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace( '"' , '\'' ).Replace( "\r\n" , @"\n" );

                System.Windows.Browser.HtmlPage.Window.Eval( "throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");" );
            }
            catch( Exception )
            {
            }
        }
    }
}

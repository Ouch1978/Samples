using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Wpf_PureApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DirectoryInfo dir = new DirectoryInfo( AppDomain.CurrentDomain.BaseDirectory );

            IEnumerable<FileInfo> fileList = dir.GetFiles( "*.dll" , System.IO.SearchOption.AllDirectories );

            var languageResourceFiles = fileList.Where( f => f.Name.Contains( string.Format( "{0}.resources" , "Wpf_PureApp" ) ) ).Select( f => f.DirectoryName.Substring( f.DirectoryName.LastIndexOf( "\\" ) + 1 ) ).ToList();

            this.comboBox.ItemsSource = languageResourceFiles;
        }

        private void comboBox_SelectionChanged( object sender , SelectionChangedEventArgs e )
        {
            CultureInfo culture = new CultureInfo( this.comboBox.SelectedValue.ToString() );
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Wpf.GridLengthAnimation.Sample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnShowHideCol_Click( object sender , RoutedEventArgs e )
        {
            ( sender as Button ).IsEnabled = false;

            BeginStoryboard( ( column1.Width.Value == 300 ) ? FindResource( "hideColumn" ) as Storyboard : FindResource( "showColumn" ) as Storyboard );
        }

        private void Storyboard_Completed( object sender , System.EventArgs e )
        {
            btnShowHideCol.IsEnabled = true;
        }
    }
}

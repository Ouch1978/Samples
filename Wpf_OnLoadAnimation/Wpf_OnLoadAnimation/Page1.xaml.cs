using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Wpf_OnLoadAnimation
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();

        }

        private void btnAddGrid_Click( object sender , System.Windows.RoutedEventArgs e )
        {
            Grid myGrid = new Grid { Background = Brushes.Red , Width = 100 , Height = 100 , Margin = new Thickness( 2 ) };

            myGrid.Loaded += new RoutedEventHandler( MyGrid_Loaded );

            wrpPanel.Children.Add( myGrid );
        }

        void MyGrid_Loaded( object sender , RoutedEventArgs e )
        {
            Grid grid = sender as Grid;

            DoubleAnimation showAnimation = new DoubleAnimation { Duration = new Duration( TimeSpan.FromMilliseconds( 1000 ) ) , };
            showAnimation.From = 0;
            showAnimation.To = 1;
            Storyboard.SetTargetProperty( showAnimation , new PropertyPath( Grid.OpacityProperty ) );
            Storyboard strbStoryBoard = new Storyboard();
            strbStoryBoard.Children.Add( showAnimation );
            strbStoryBoard.Begin( grid );
        }

        private void btnRemoveLast_Click( object sender , RoutedEventArgs e )
        {
            if( this.wrpPanel.Children.Count > 0 )
            {
                this.IsEnabled = false;

                Grid grid = this.wrpPanel.Children.OfType<Grid>().Last() as Grid;

                DoubleAnimation hideAnimation = new DoubleAnimation { Duration = new Duration( TimeSpan.FromMilliseconds( 300 ) ) , };
                hideAnimation.From = 1;
                hideAnimation.To = 0;
                Storyboard.SetTargetProperty( hideAnimation , new PropertyPath( Grid.OpacityProperty ) );
                Storyboard strbStoryBoard = new Storyboard();
                strbStoryBoard.Children.Add( hideAnimation );

                strbStoryBoard.Completed += new EventHandler( strbStoryBoard_Completed );

                strbStoryBoard.Begin( grid );

            }

        }

        void strbStoryBoard_Completed( object sender , EventArgs e )
        {
            this.wrpPanel.Children.RemoveAt( this.wrpPanel.Children.Count - 1 );

            this.IsEnabled = true;
        }
    }
}

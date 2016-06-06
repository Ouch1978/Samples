using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Sl_GridLengthAnimation
{
    public partial class MainPage : UserControl
    {
        private bool _isColumnCollapsed = false;

        public MainPage()
        {
            // Required to initialize variables
            InitializeComponent();
        }

        public static readonly DependencyProperty GridWidthProperty = DependencyProperty.Register( "GridWidth" , typeof( double ) , typeof( MainPage ) ,
            new PropertyMetadata( ( double ) 100.00 , OnGridWidthPropertyChanged ) );

        public double GridWidth
        {
            get
            {
                return ( double ) GetValue( GridWidthProperty );
            }
            set
            {
                SetValue( GridWidthProperty , value );
            }
        }

        private static void OnGridWidthPropertyChanged( DependencyObject obj , DependencyPropertyChangedEventArgs args )
        {
            if( obj != null )
            {
                MainPage mainPage = obj as MainPage;

                mainPage.column1.Width = new GridLength( mainPage.GridWidth , GridUnitType.Pixel );
            }
        }


        private void btnShowHide_Click( object sender , System.Windows.RoutedEventArgs e )
        {
            btnShowHide.IsEnabled = false;

            Storyboard storyboard = ( this._isColumnCollapsed == true ) ? this.Resources[ "ShowColumn" ] as Storyboard : this.Resources[ "HideColumn" ] as Storyboard;
			
            storyboard.Stop();

            storyboard.Begin();
        }

        private void DoubleAnimation_Completed( object sender , EventArgs e )
        {
            this._isColumnCollapsed = !this._isColumnCollapsed;
            btnShowHide.IsEnabled = true;

            btnShowHide.Content = new TextBlock { Text = ( this._isColumnCollapsed == true ) ? "Show Column" : "Hide Column" };
        }

    }
}
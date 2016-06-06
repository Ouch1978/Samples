using System.Windows;

namespace Wpf_GoToStateSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Button_Click( object sender , RoutedEventArgs e )
        {
            if( this.twoStateControl.Visibility == Visibility.Visible )
            {
                //這個寫法完全起不了作用
                //VisualStateManager.GoToState( this , "State1" , true );

                Microsoft.Expression.Interactivity.Core.ExtendedVisualStateManager.GoToElementState( this.LayoutRoot , "State1" , true );
            }
            else
            {
                //這個寫法完全起不了作用
                //VisualStateManager.GoToState( this , "State2" , true );
                VisualStateManager.GoToElementState( this.LayoutRoot , "State2" , true );

            }

        }

    }
}
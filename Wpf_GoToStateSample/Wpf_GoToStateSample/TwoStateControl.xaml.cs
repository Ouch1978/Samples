using System.Windows;
using System.Windows.Controls;

namespace Wpf_GoToStateSample
{
    public partial class TwoStateControl : UserControl
    {
        public TwoStateControl()
        {
            this.InitializeComponent();
        }

        private void Button_Click( object sender , RoutedEventArgs e )
        {
            if( this.rectangle.Visibility == Visibility.Visible )
            {
                //這邊用VisualStateManager.GoToState可以跑得很開心
                VisualStateManager.GoToState( this , "State2" , true );
            }
            else
            {
                //這邊用VisualStateManager.GoToState可以跑得很開心
                VisualStateManager.GoToState( this , "State1" , true );
            }
        }
    }
}
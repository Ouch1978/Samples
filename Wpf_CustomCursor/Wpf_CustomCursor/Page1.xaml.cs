using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Wpf_CustomCursor
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

        private void btnChangeCursor1_Click( object sender , RoutedEventArgs e )
        {
            this.Cursor = CursorHelper.CreateCursor( new CustomCursor1() );
        }

        private void btnChangeCursor2_Click( object sender , RoutedEventArgs e )
        {
            this.Cursor = CursorHelper.CreateCursor( new CustomCursor2() );
        }

        private void btnChangeCursor3_Click( object sender , RoutedEventArgs e )
        {
            this.Cursor = CursorHelper.CreateCursor( new CustomCursor3() );
        }

        private void btnResetCursor_Click( object sender , RoutedEventArgs e )
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}

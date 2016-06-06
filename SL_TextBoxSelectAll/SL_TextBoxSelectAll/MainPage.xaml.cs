using System.Windows;
using System.Windows.Controls;

namespace SL_TextBoxSelectAll
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            //只要幫TextBox的GotFocus觸發時執行SelectAll()方法即可
            txtAutoSelectAll.GotFocus += SelectAllText;
        }

        private void SelectAllText( object sender , RoutedEventArgs e )
        {
            var textBox = e.OriginalSource as TextBox;

            if( textBox != null )
            {
                textBox.SelectAll();
            }
        }

    }
}

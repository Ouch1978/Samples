using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Wpf_TextBoxSelectAll
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();

            txtAutoSelectAll.GotFocus += SelectAllText;
            txtNormal.GotFocus += SelectAllText;
            //得在放開滑鼠左鍵的事件中多做點事才行~
            txtAutoSelectAll.PreviewMouseLeftButtonDown += TextBox_PreviewMouseLeftButtonDown;
        }

        private void TextBox_PreviewMouseLeftButtonDown( object sender , MouseButtonEventArgs e )
        {
            var textbox = ( sender as TextBox );

            //多判斷鍵盤的焦點是否在被選到的TextBox上
            if( textbox != null && textbox.IsKeyboardFocusWithin == false )
            {
                //觸發事件的東西居然是TextBoxView，而不是TextBox本身喔!!
                if( e.OriginalSource.GetType().Name == "TextBoxView" )
                {
                    e.Handled = true;
                    textbox.Focus();
                }
            }
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

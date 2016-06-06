using System.Windows;


namespace WPF_Winform
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click( object sender , System.EventArgs e )
        {
            System.Windows.MessageBox.Show( "你點了Winform的按鈕" );
        }

        private void Button_Click( object sender , RoutedEventArgs e )
        {
            System.Windows.MessageBox.Show( "你點了WPF的按鈕" );
        }
    }
}

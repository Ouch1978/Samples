using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CS_ReplaceForeachWithLinq
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click( object sender , RoutedEventArgs e )
        {
            //在使用ForEach方法之前，可能得用像以下的寫法
            /*
            var textBoxes = this.LayoutRoot.Children.OfType<TextBox>().Where( t => t.Text.Length == 0 );

            foreach( TextBox textBox in textBoxes )
            {
                textBox.BorderBrush = new SolidColorBrush { Color = Colors.Red }; 
                textBox.Background = new SolidColorBrush { Color = Colors.Orange };
            }
            */

            //在沒使用ExtensionMethod之前，得先轉成List才可以使用ForEach方法
            this.LayoutRoot.Children.OfType<TextBox>().Where( t => t.Text.Length == 0 ).ToList<TextBox>().ForEach( t => { t.BorderBrush = new SolidColorBrush { Color = Colors.Red }; t.Background = new SolidColorBrush { Color = Colors.Orange }; } );
        }

        private void btnReset_Click( object sender , RoutedEventArgs e )
        {
            //使用ExtensionMethod，只要一行就可以搞定了
            this.LayoutRoot.Children.OfType<TextBox>().ForEach( t => { t.Text = string.Empty; t.Background = new SolidColorBrush { Color = Colors.White }; t.BorderBrush = new SolidColorBrush { Color = Colors.Black }; } );
        }
    }
}

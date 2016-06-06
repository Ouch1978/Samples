using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Wpf_ColorConverter
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

        private void btnApplyColor1_Click( object sender , RoutedEventArgs e )
        {
            try
            {
                recColor1.Fill = new SolidColorBrush( Color.FromArgb( Convert.ToByte( txtColor1_Alpha.Text ) , Convert.ToByte( txtColor1_Red.Text ) ,
                    Convert.ToByte( txtColor1_Green.Text ) , Convert.ToByte( txtColor1_Blue.Text ) ) );
            }
            catch( Exception )
            {
                MessageBox.Show( "色碼或顏色設定錯誤" );
            }
        }

        private void btnApplyColor2_Click( object sender , RoutedEventArgs e )
        {
            try
            {
                recColor2.Fill = new SolidColorBrush( ( System.Windows.Media.Color ) System.Windows.Media.ColorConverter.ConvertFromString( txtColor2.Text ) );
            }
            catch( Exception )
            {
                MessageBox.Show( "色碼或顏色設定錯誤" );
            }
        }

        private void btnApplyColor3_Click( object sender , RoutedEventArgs e )
        {
            try
            {
                BrushConverter brushConverter = new BrushConverter();

                recColor3.Fill = brushConverter.ConvertFromString( txtColor3.Text ) as Brush;
            }
            catch( Exception )
            {
                MessageBox.Show( "色碼或顏色設定錯誤" );
            }
        }
    }
}

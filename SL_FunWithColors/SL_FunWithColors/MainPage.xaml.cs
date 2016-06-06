using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SL_FunWithColors
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private void btnApplyColor1_Click( object sender , System.Windows.RoutedEventArgs e )
        {
            try
            {
                recColor1.Fill = new SolidColorBrush( Color.FromArgb( Convert.ToByte( txtColor1_Alpha.Text , 16 ) , Convert.ToByte( txtColor1_Red.Text , 16 ) ,
                    Convert.ToByte( txtColor1_Green.Text , 16 ) , Convert.ToByte( txtColor1_Blue.Text , 16 ) ) );
            }
            catch
            {
                MessageBox.Show( "色碼或顏色設定錯誤" );
            }
        }

        private void btnApplyColor2_Click( object sender , System.Windows.RoutedEventArgs e )
        {
            Color color = ( Color ) new ColorConverter().Convert( txtColor2.Text , null , null , null );

            recColor2.Fill = new SolidColorBrush( color );
        }

        private void btnApplyColor3_Click( object sender , RoutedEventArgs e )
        {
            recColor3.Fill = new SolidColorBrush( XamlColorConverter.ConvertFromString( txtColor3.Text ) );
        }
    }
}

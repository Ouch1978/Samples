using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SL_RandomWithLinq
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            for( int i = 1 ; i <= 49 ; i++ )
            {
                this.wrpNumbers.Children.Add( new TextBlock
                                                {
                                                    Text = i.ToString( "00" ) ,
                                                    Foreground = new SolidColorBrush { Color = Colors.Gray } ,
                                                    FontSize = 32 ,
                                                    Width = 80 ,
                                                    Margin = new Thickness( 5 ) ,
                                                    TextAlignment = TextAlignment.Center ,
                                                } );
            }
        }

        private void btnRandom_Click( object sender , RoutedEventArgs e )
        {
            //重置所有號碼為灰色
            this.wrpNumbers.Children.OfType<TextBlock>().ToList().ForEach( t => t.Foreground = new SolidColorBrush { Color = Colors.Gray } );

            //將數值打亂之後透過Distinct取出不重複的值，再透過Take取出6個值
            var result = Enumerable.Range( 1 , 49 ).OrderBy( n => n * n * ( new Random() ).Next() ).Distinct().Take( 6 );

            //將被挑選出來的號碼改為紅色
            result.ToList().ForEach( i => this.wrpNumbers.Children.OfType<TextBlock>().ElementAt( i - 1 ).Foreground = new SolidColorBrush { Color = Colors.Red } );
        }


    }
}
